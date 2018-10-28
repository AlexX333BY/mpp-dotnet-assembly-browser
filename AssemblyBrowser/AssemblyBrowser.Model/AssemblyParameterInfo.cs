using System;
using System.Reflection;

namespace AssemblyBrowser.Model
{
    public class AssemblyParameterInfo
    {
        protected readonly ParameterInfo parameterInfo;

        public string Name => parameterInfo.Name;

        public string Type => parameterInfo.ParameterType.Name;

        public bool IsIn => parameterInfo.IsIn;

        public bool IsOut => parameterInfo.IsOut;

        public bool IsRef => parameterInfo.ParameterType.IsByRef && !parameterInfo.IsOut;

        public bool HasDefaultValue => parameterInfo.IsOptional;

        public object DefaultValue => parameterInfo.DefaultValue;

        public AssemblyParameterInfo(ParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo ?? throw new ArgumentException("Parameter info shouldn't be null");
        }
    }
}
