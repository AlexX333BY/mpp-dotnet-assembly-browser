using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Model
{
    public class AssemblyMethodInfo : IAccessable, IGenericable
    {
        protected List<AssemblyParameterInfo> methodParameters;
        protected List<AssemblyGenericParameterInfo> genericParameters;
        protected readonly MethodInfo methodInfo;

        public string Name => methodInfo.Name;

        public string ReturnType => methodInfo.ReturnType.Name;

        public List<AssemblyParameterInfo> Parameters
        {
            get
            {
                if (methodParameters == null)
                {
                    methodParameters = new List<AssemblyParameterInfo>();
                    foreach (ParameterInfo parameter in methodInfo.GetParameters())
                    {
                        methodParameters.Add(new AssemblyParameterInfo(parameter));
                    }
                }
                return new List<AssemblyParameterInfo>(methodParameters);
            }
        }

        public bool IsPublic => methodInfo.IsPublic;

        public bool IsProtected => methodInfo.IsFamily;

        public bool IsInternal => methodInfo.IsAssembly;

        public bool IsProtectedInternal => methodInfo.IsFamilyOrAssembly;

        public bool IsPrivate => methodInfo.IsPrivate;

        public bool IsPrivateProtected => methodInfo.IsFamilyAndAssembly;

        public bool IsAbstract => methodInfo.IsAbstract && !methodInfo.DeclaringType.IsInterface;

        public bool IsAsync => methodInfo.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;

        public bool IsExtern => (methodInfo.MethodImplementationFlags & MethodImplAttributes.InternalCall) != 0;

        public bool IsOverriden => !methodInfo.Equals(methodInfo.GetBaseDefinition());

        public bool IsSealed => methodInfo.IsVirtual && methodInfo.IsFinal;

        public bool IsStatic => methodInfo.IsStatic;

        public bool IsVirtual => methodInfo.IsVirtual && !methodInfo.IsFinal && !IsOverriden && !methodInfo.DeclaringType.IsInterface;

        public bool IsGeneric => methodInfo.IsGenericMethod;

        public List<AssemblyGenericParameterInfo> GenericParameters
        {
            get
            {
                if (genericParameters == null)
                {
                    genericParameters = new List<AssemblyGenericParameterInfo>();
                    foreach (Type type in methodInfo.GetGenericArguments())
                    {
                        genericParameters.Add(new AssemblyGenericParameterInfo(type));
                    }
                }
                return new List<AssemblyGenericParameterInfo>(genericParameters);
            }
        }

        public AssemblyMethodInfo(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo ?? throw new ArgumentException("Method info shouldn't be null");
            methodParameters = null;
            genericParameters = null;
        }
    }
}
