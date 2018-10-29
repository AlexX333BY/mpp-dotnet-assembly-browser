using System.Collections.Generic;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class GenericParameterStringProcessor : IDeclaredElementStringProcessor
    {
        protected readonly AssemblyGenericParameterInfo genericParameterInfo;

        public string GetDeclaration(string modifiersDelimiter)
        {
            if (genericParameterInfo.IsGeneric)
            {
                List<string> subparameters = new List<string>();
                foreach (AssemblyGenericParameterInfo subparameter in genericParameterInfo.GenericParameters)
                {
                    subparameters.Add(new GenericParameterStringProcessor(subparameter).GetDeclaration(modifiersDelimiter));
                }
                return genericParameterInfo.Name + "<" + string.Join("," + modifiersDelimiter, subparameters) + ">";
            }
            else
            {
                return genericParameterInfo.Name;
            }
        }

        public GenericParameterStringProcessor(AssemblyGenericParameterInfo genericParameterInfo)
        {
            this.genericParameterInfo = genericParameterInfo;
        }
    }
}
