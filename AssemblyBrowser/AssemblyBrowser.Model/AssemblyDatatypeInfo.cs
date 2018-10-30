using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AssemblyBrowser.Model
{
    public class AssemblyDatatypeInfo : IAccessable, IGenericParametrizable
    {
        protected readonly Type typeInfo;
        protected List<AssemblyFieldInfo> datatypeFields;
        protected List<AssemblyPropertyInfo> datatypeProperties;
        protected List<AssemblyMethodInfo> datatypeMethods;
        protected List<AssemblyDatatypeInfo> interfaces;
        protected List<AssemblyGenericParameterInfo> genericParameters;
        protected List<AssemblyMethodInfo> extensionMethods;

        public List<AssemblyFieldInfo> Fields
        {
            get
            {
                if (datatypeFields == null)
                {
                    datatypeFields = new List<AssemblyFieldInfo>();
                    AssemblyFieldInfo fieldInfo;
                    Regex backingFieldRegex = new Regex(@"^\<.*\>.*BackingField$", RegexOptions.Compiled);
                    foreach (FieldInfo field in typeInfo.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        fieldInfo = new AssemblyFieldInfo(field);
                        if (!backingFieldRegex.IsMatch(fieldInfo.Name))
                        {
                            datatypeFields.Add(fieldInfo);
                        }
                    }
                }
                return new List<AssemblyFieldInfo>(datatypeFields);
            }
        }

        public List<AssemblyPropertyInfo> Properties
        {
            get
            {
                if (datatypeProperties == null)
                {
                    datatypeProperties = new List<AssemblyPropertyInfo>();
                    foreach (PropertyInfo property in typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        datatypeProperties.Add(new AssemblyPropertyInfo(property));
                    }
                }
                return new List<AssemblyPropertyInfo>(datatypeProperties);
            }
        }

        public List<AssemblyMethodInfo> Methods
        {
            get
            {
                if (datatypeMethods == null)
                {
                    datatypeMethods = new List<AssemblyMethodInfo>();
                    AssemblyMethodInfo addMethodInfo;
                    foreach (MethodInfo method in typeInfo.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        addMethodInfo = new AssemblyMethodInfo(method);
                        if (!addMethodInfo.Name.StartsWith("get_") && !addMethodInfo.Name.StartsWith("set_") && !addMethodInfo.IsExtensionMethod)
                        {
                            datatypeMethods.Add(addMethodInfo);
                        }
                    }
                }
                return new List<AssemblyMethodInfo>(datatypeMethods);
            }
        }

        public List<AssemblyDatatypeInfo> Interfaces
        {
            get
            {
                if (interfaces == null)
                {
                    interfaces = new List<AssemblyDatatypeInfo>();
                    foreach (Type type in typeInfo.GetInterfaces())
                    {
                        interfaces.Add(new AssemblyDatatypeInfo(type));
                    }
                }
                return new List<AssemblyDatatypeInfo>(interfaces);
            }
        }

        public AssemblyDatatypeInfo BaseType => new AssemblyDatatypeInfo(typeInfo.BaseType);

        public string Name => typeInfo.Name;

        public bool IsPublic => typeInfo.IsPublic;

        public bool IsProtected => typeInfo.GetTypeInfo().IsNestedFamily;

        public bool IsInternal => typeInfo.GetTypeInfo().IsNestedAssembly;

        public bool IsProtectedInternal => typeInfo.GetTypeInfo().IsNestedFamORAssem;

        public bool IsPrivate => typeInfo.GetTypeInfo().IsNestedPrivate;

        public bool IsPrivateProtected => typeInfo.GetTypeInfo().IsNestedFamANDAssem;

        public bool IsAbstract => typeInfo.IsAbstract && !IsStatic;

        public bool IsSealed => typeInfo.IsSealed && !IsStatic;

        public bool IsStatic => typeInfo.IsAbstract && typeInfo.IsSealed;

        public bool IsInterface => typeInfo.IsInterface;

        public bool IsEnum => typeInfo.IsEnum;

        public bool IsStruct => typeInfo.IsValueType && !IsEnum;

        public bool IsClass => typeInfo.IsClass && !IsInterface;

        public bool IsGenericParametrized => typeInfo.IsGenericType;

        public List<AssemblyGenericParameterInfo> GenericParameters
        {
            get
            {
                if (genericParameters == null)
                {
                    genericParameters = new List<AssemblyGenericParameterInfo>();
                    foreach (Type type in typeInfo.GetGenericArguments())
                    {
                        genericParameters.Add(new AssemblyGenericParameterInfo(type));
                    }
                }
                return new List<AssemblyGenericParameterInfo>(genericParameters);
            }
        }

        public List<AssemblyMethodInfo> ExtensionMethods
        {
            get
            {
                if (extensionMethods == null)
                {
                    return null;
                }
                else
                {
                    return new List<AssemblyMethodInfo>(extensionMethods);
                }
            }
        }

        protected void SetExtensionMethods(List<MethodInfo> rawExtensionMethods)
        {
            extensionMethods = new List<AssemblyMethodInfo>();
            if (rawExtensionMethods != null)
            {
                foreach (MethodInfo methodInfo in rawExtensionMethods)
                {
                    extensionMethods.Add(new AssemblyMethodInfo(methodInfo));
                }
            }
        }

        public AssemblyDatatypeInfo(Type typeInfo, List<MethodInfo> extensionMethods = null)
        {
            this.typeInfo = typeInfo ?? throw new ArgumentException("Type info shouldn't be null");
            datatypeFields = null;
            datatypeProperties = null;
            datatypeMethods = null;
            interfaces = null;
            genericParameters = null;
            SetExtensionMethods(extensionMethods);
        }
    }
}
