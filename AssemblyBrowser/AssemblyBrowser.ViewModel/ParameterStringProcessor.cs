using System.Collections.Generic;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class ParameterStringProcessor : IDeclaredElementStringProcessor
    {
        protected readonly AssemblyParameterInfo parameterInfo;

        public string GetDeclaration(string modifiersDelimiter)
        {
            List<string> parameter = new List<string>();
            if (parameterInfo.IsIn)
            {
                parameter.Add("in");
            }
            if (parameterInfo.IsOut)
            {
                parameter.Add("out");
            }
            if (parameterInfo.IsRef)
            {
                parameter.Add("ref");
            }

            parameter.Add(parameterInfo.Name);
            if (parameterInfo.HasDefaultValue)
            {
                parameter.Add(string.Format(" = {0}", parameterInfo.DefaultValue));
            }
            return string.Join(modifiersDelimiter, parameter);
        }

        public ParameterStringProcessor(AssemblyParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo;
        }
    }
}
