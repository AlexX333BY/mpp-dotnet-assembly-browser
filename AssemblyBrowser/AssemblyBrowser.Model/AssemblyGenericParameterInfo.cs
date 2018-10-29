using System;
using System.Collections.Generic;

namespace AssemblyBrowser.Model
{
    public class AssemblyGenericParameterInfo
    {
        protected readonly Type typeInfo;
        protected List<AssemblyGenericParameterInfo> genericParameters;

        public string Name => typeInfo.Name;

        public bool IsGeneric => typeInfo.IsGenericType;

        public List<AssemblyGenericParameterInfo> GenericParameters
        {
            get
            {
                if (genericParameters == null)
                {
                    genericParameters = new List<AssemblyGenericParameterInfo>();
                    foreach (Type parameter in typeInfo.GetGenericArguments())
                    {
                        genericParameters.Add(new AssemblyGenericParameterInfo(parameter));
                    }
                }
                return new List<AssemblyGenericParameterInfo>(genericParameters);
            }
        }

        public AssemblyGenericParameterInfo(Type type)
        {
            typeInfo = type ?? throw new ArgumentException("Type shouldn't be null");
        }
    }
}
