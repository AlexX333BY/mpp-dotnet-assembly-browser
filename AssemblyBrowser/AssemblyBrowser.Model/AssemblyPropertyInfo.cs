﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser.Model
{
    public class AssemblyPropertyInfo : IReturnTypeGenericable
    {
        protected readonly PropertyInfo propertyInfo;
        protected List<AssemblyGenericParameterInfo> returnTypeGenericParameters;

        public string Name => propertyInfo.Name;

        public string Type => propertyInfo.PropertyType.Name;

        public bool HasGetter => propertyInfo.CanRead;

        public bool HasSetter => propertyInfo.CanWrite;

        public bool IsGetterPublic => HasGetter && propertyInfo.GetGetMethod(true).IsPublic;

        public bool IsGetterProtected => HasGetter && propertyInfo.GetGetMethod(true).IsFamily;

        public bool IsGetterInternal => HasGetter && propertyInfo.GetGetMethod(true).IsAssembly;

        public bool IsGetterProtectedInternal => HasGetter && propertyInfo.GetGetMethod(true).IsFamilyOrAssembly;

        public bool IsGetterPrivate => HasGetter && propertyInfo.GetGetMethod(true).IsPrivate;

        public bool IsGetterPrivateProtected => HasGetter && propertyInfo.GetGetMethod(true).IsFamilyAndAssembly;

        public bool IsSetterPublic => HasSetter && propertyInfo.GetSetMethod(true).IsPublic;

        public bool IsSetterProtected => HasSetter && propertyInfo.GetSetMethod(true).IsFamily;

        public bool IsSetterInternal => HasSetter && propertyInfo.GetSetMethod(true).IsAssembly;

        public bool IsSetterProtectedInternal => HasSetter && propertyInfo.GetSetMethod(true).IsFamilyOrAssembly;

        public bool IsSetterPrivate => HasSetter && propertyInfo.GetSetMethod(true).IsPrivate;

        public bool IsSetterPrivateProtected => HasSetter && propertyInfo.GetSetMethod(true).IsFamilyAndAssembly;

        public bool IsAbstract
        {
            get
            {
                if (propertyInfo.DeclaringType.IsInterface)
                {
                    return false;
                }

                MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
                if (getMethodInfo != null)
                {
                    return getMethodInfo.IsAbstract;
                }
                return propertyInfo.GetSetMethod(true).IsAbstract;
            }
        }

        public bool IsOverriden
        {
            get
            {
                MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
                if (methodInfo != null)
                {
                    return !methodInfo.Equals(methodInfo.GetBaseDefinition());
                }
                methodInfo = propertyInfo.GetSetMethod(true);
                return !methodInfo.Equals(methodInfo.GetBaseDefinition());
            }
        }

        public bool IsSealed
        {
            get
            {
                MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
                if (methodInfo != null)
                {
                    return methodInfo.IsVirtual && methodInfo.IsFinal;
                }
                methodInfo = propertyInfo.GetSetMethod(true);
                return methodInfo.IsVirtual && methodInfo.IsFinal;
            }
        }

        public bool IsStatic
        {
            get
            {
                MethodInfo getMethodInfo = propertyInfo.GetGetMethod(true);
                if (getMethodInfo != null)
                {
                    return getMethodInfo.IsStatic;
                }
                return propertyInfo.GetSetMethod(true).IsStatic;
            }
        }

        public bool IsVirtual
        {
            get
            {
                if (propertyInfo.DeclaringType.IsInterface)
                {
                    return false;
                }

                MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
                if (methodInfo != null)
                {
                    return methodInfo.IsVirtual && !methodInfo.IsFinal && !IsOverriden;
                }
                methodInfo = propertyInfo.GetSetMethod(true);
                return methodInfo.IsVirtual && !methodInfo.IsFinal && !IsOverriden;
            }
        }

        public bool IsReturnTypeGeneric => propertyInfo.PropertyType.IsGenericType;

        public List<AssemblyGenericParameterInfo> ReturnTypeGenericParameters
        {
            get
            {
                if (returnTypeGenericParameters == null)
                {
                    returnTypeGenericParameters = new List<AssemblyGenericParameterInfo>();
                    foreach (Type type in propertyInfo.PropertyType.GetGenericArguments())
                    {
                        returnTypeGenericParameters.Add(new AssemblyGenericParameterInfo(type));
                    }
                }
                return new List<AssemblyGenericParameterInfo>(returnTypeGenericParameters);
            }
        }

        public AssemblyPropertyInfo(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo ?? throw new ArgumentException("Property info shouldn't be null");
            returnTypeGenericParameters = null;
        }
    }
}
