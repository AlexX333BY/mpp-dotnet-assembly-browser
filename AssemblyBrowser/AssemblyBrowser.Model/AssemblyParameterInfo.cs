using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser.Model
{
    public class AssemblyParameterInfo : IGenericParametrizable
    {
        protected readonly ParameterInfo parameterInfo;
        protected List<AssemblyGenericParameterInfo> genericParameters;

        public string Name => parameterInfo.Name;

        public string Type => parameterInfo.ParameterType.Name;

        public bool IsIn => parameterInfo.IsIn;

        public bool IsOut => parameterInfo.IsOut;

        public bool IsRef => parameterInfo.ParameterType.IsByRef && !parameterInfo.IsOut;

        public bool HasDefaultValue => parameterInfo.IsOptional;

        public object DefaultValue => parameterInfo.DefaultValue;

        public bool IsGenericParametrized => parameterInfo.ParameterType.IsGenericType;

        public List<AssemblyGenericParameterInfo> GenericParameters
        {
            get
            {
                if (genericParameters == null)
                {
                    genericParameters = new List<AssemblyGenericParameterInfo>();
                    foreach (Type type in parameterInfo.ParameterType.GetGenericArguments())
                    {
                        genericParameters.Add(new AssemblyGenericParameterInfo(type));
                    }
                }
                return new List<AssemblyGenericParameterInfo>(genericParameters);
            }
        }

        public AssemblyParameterInfo(ParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo ?? throw new ArgumentException("Parameter info shouldn't be null");
            genericParameters = null;
        }
    }
}
