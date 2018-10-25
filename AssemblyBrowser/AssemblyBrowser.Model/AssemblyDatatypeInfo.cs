using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser.Model
{
    public class AssemblyDatatypeInfo
    {
        protected readonly Type typeInfo;
        protected List<AssemblyFieldInfo> datatypeFields;
        protected List<AssemblyPropertyInfo> datatypeProperties;
        protected List<AssemblyMethodInfo> datatypeMethods;

        public List<AssemblyFieldInfo> Fields
        {
            get
            {
                if (datatypeFields == null)
                {
                    datatypeFields = new List<AssemblyFieldInfo>();
                    foreach (FieldInfo field in typeInfo.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        datatypeFields.Add(new AssemblyFieldInfo(field));
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
                    foreach (MethodInfo method in typeInfo.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        datatypeMethods.Add(new AssemblyMethodInfo(method));
                    }
                }
                return new List<AssemblyMethodInfo>(datatypeMethods);
            }
        }

        public string Name => typeInfo.Name;

        public bool IsPublic => typeInfo.IsPublic;

        public bool IsProtected => typeInfo.GetTypeInfo().IsNestedFamily;

        public bool IsInternal => typeInfo.GetTypeInfo().IsNestedAssembly;

        public bool IsProtectedInternal => typeInfo.GetTypeInfo().IsNestedFamORAssem;

        public bool IsPrivate => typeInfo.GetTypeInfo().IsNestedPrivate;

        public bool IsPrivateProtected => typeInfo.GetTypeInfo().IsNestedFamANDAssem;

        public bool IsAbstract => typeInfo.IsAbstract;

        public bool IsSealed => typeInfo.IsSealed;

        public bool IsStatic => typeInfo.IsAbstract && typeInfo.IsSealed;

        public AssemblyDatatypeInfo(Type typeInfo)
        {
            this.typeInfo = typeInfo ?? throw new ArgumentException("Type info shouldn't be null");
            datatypeFields = null;
            datatypeProperties = null;
            datatypeMethods = null;
        }
    }
}
