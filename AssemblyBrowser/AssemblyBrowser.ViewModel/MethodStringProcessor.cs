using System.Collections.Generic;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class MethodStringProcessor : IDeclaredElementStringProcessor
    {
        protected readonly AssemblyMethodInfo methodInfo;

        public string GetDeclaration(string modifiersDelimiter)
        {
            List<string> modifiers = new List<string>
            {
                AssemblyStringProcessor.CreateAccessModifier(methodInfo)
            };

            if (methodInfo.IsStatic)
            {
                modifiers.Add("static");
            }
            if (methodInfo.IsAbstract)
            {
                modifiers.Add("abstract");
            }
            if (methodInfo.IsAsync)
            {
                modifiers.Add("async");
            }
            if (methodInfo.IsExtern)
            {
                modifiers.Add("extern");
            }
            if (methodInfo.IsOverriden)
            {
                modifiers.Add("override");
            }
            if (methodInfo.IsSealed)
            {
                modifiers.Add("sealed");
            }
            if (methodInfo.IsVirtual)
            {
                modifiers.Add("virtual");
            }

            modifiers.Add(methodInfo.ReturnType + GetReturnTypeGenericParametersDeclaration(modifiersDelimiter));
            modifiers.Add(methodInfo.Name + GetMethodGenericParametersDeclaration(modifiersDelimiter));
            return string.Format("{0}({1})", string.Join(modifiersDelimiter, modifiers), CreateParameters(modifiersDelimiter));
        }

        protected string CreateParameters(string modifiersDelimiter)
        {
            List<string> parameters = new List<string>();
            foreach (AssemblyParameterInfo parameterInfo in methodInfo.Parameters)
            {
                parameters.Add(new ParameterStringProcessor(parameterInfo).GetDeclaration(modifiersDelimiter));
            }
            return string.Join(string.Format(",{0}", modifiersDelimiter), parameters);
        }

        protected string GetMethodGenericParametersDeclaration(string parametersDelimiter)
        {
            if (methodInfo.IsMethodGeneric)
            {
                List<string> parameters = new List<string>();
                foreach (AssemblyGenericParameterInfo genericParameter in methodInfo.MethodGenericParameters)
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

        protected string GetReturnTypeGenericParametersDeclaration(string parametersDelimiter)
        {
            if (methodInfo.IsReturnTypeGeneric)
            {
                List<string> parameters = new List<string>();
                foreach (AssemblyGenericParameterInfo genericParameter in methodInfo.ReturnTypeGenericParameters)
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

        public MethodStringProcessor(AssemblyMethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }
    }
}
