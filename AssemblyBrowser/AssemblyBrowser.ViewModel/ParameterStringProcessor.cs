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

            parameter.Add(parameterInfo.Type + GetGenericParametersDeclaration(modifiersDelimiter));
            parameter.Add(parameterInfo.Name);
            if (parameterInfo.HasDefaultValue)
            {
                parameter.Add(string.Format("={0}{1}", modifiersDelimiter, parameterInfo.DefaultValue.ToString()));
            }
            return string.Join(modifiersDelimiter, parameter);
        }

        protected string GetGenericParametersDeclaration(string parametersDelimiter)
        {
            if (parameterInfo.IsGeneric)
            {
                List<string> parameters = new List<string>();
                foreach (AssemblyGenericParameterInfo genericParameter in parameterInfo.GenericParameters)
                {
                    parameters.Add(new GenericParameterStringProcessor(genericParameter).GetDeclaration(parametersDelimiter));
                }
                return "<" + string.Join("," + parametersDelimiter, parameters) + ">";
            }
            else
            {
                return "";
            }
        }

        public ParameterStringProcessor(AssemblyParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo;
        }
    }
}
