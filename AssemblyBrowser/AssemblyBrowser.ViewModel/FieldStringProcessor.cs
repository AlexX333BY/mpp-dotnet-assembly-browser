using System.Collections.Generic;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class FieldStringProcessor : IDeclaredElementStringProcessor
    {
        protected readonly AssemblyFieldInfo fieldInfo;

        protected string GetGenericParametersDeclaration(string modifiersDelimiter)
        {
            if (fieldInfo.IsGeneric)
            {
                List<string> parameters = new List<string>();
                foreach (AssemblyGenericParameterInfo genericParameter in fieldInfo.GenericParameters)
                {
                    parameters.Add(new GenericParameterStringProcessor(genericParameter).GetDeclaration(modifiersDelimiter));
                }
                return "<" + string.Join("," + modifiersDelimiter, parameters) + ">";
            }
            else
            {
                return "";
            }
        }

        public string GetDeclaration(string modifiersDelimiter)
        {
            List<string> modifiers = new List<string>
            {
                AssemblyStringProcessor.CreateAccessModifier(fieldInfo)
            };

            if (fieldInfo.IsStatic)
            {
                modifiers.Add("static");
            }
            if (fieldInfo.IsConst)
            {
                modifiers.Add("const");
            }
            if (fieldInfo.IsReadonly)
            {
                modifiers.Add("readonly");
            }
            if (fieldInfo.IsVolatile)
            {
                modifiers.Add("volatile");
            }

            modifiers.Add(fieldInfo.Type + GetGenericParametersDeclaration(modifiersDelimiter));
            modifiers.Add(fieldInfo.Name);
            return string.Join(modifiersDelimiter, modifiers);
        }

        public FieldStringProcessor(AssemblyFieldInfo fieldInfo)
        {
            this.fieldInfo = fieldInfo;
        }
    }
}
