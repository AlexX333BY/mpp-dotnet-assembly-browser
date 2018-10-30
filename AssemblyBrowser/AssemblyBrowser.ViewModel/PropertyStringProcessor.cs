using System.Collections.Generic;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class PropertyStringProcessor : IDeclaredElementStringProcessor
    {
        protected readonly AssemblyPropertyInfo propertyInfo;

        public string GetDeclaration(string delimiter)
        {
            List<string> modifiers = new List<string>();

            string prefixAccessModifier = CreatePropertyPrefixAccessModifier(propertyInfo);
            if (prefixAccessModifier != "")
            {
                modifiers.Add(prefixAccessModifier);
            }

            if (propertyInfo.IsStatic)
            {
                modifiers.Add("static");
            }
            if (propertyInfo.IsAbstract)
            {
                modifiers.Add("abstract");
            }
            if (propertyInfo.IsOverriden)
            {
                modifiers.Add("override");
            }
            if (propertyInfo.IsSealed)
            {
                modifiers.Add("sealed");
            }
            if (propertyInfo.IsVirtual)
            {
                modifiers.Add("virtual");
            }

            modifiers.Add(propertyInfo.Type + GetReturnTypeGenericParametersDeclaration(delimiter));
            modifiers.Add(propertyInfo.Name);

            if (prefixAccessModifier == "")
            {
                modifiers.Add(CreatePropertyPostfixAccessModifier(propertyInfo));
            }
            else
            {
                string postfix = "{ ";
                if (propertyInfo.HasGetter)
                {
                    postfix += "get; ";
                }
                if (propertyInfo.HasSetter)
                {
                    postfix += "set; ";
                }
                postfix += "}";
                modifiers.Add(postfix);
            }

            return string.Join(delimiter, modifiers);
        }

        protected string GetReturnTypeGenericParametersDeclaration(string genericParametersDelimiter)
        {
            if (propertyInfo.IsReturnTypeGeneric)
            {
                List<string> parameters = new List<string>();
                foreach (AssemblyGenericParameterInfo genericParameter in propertyInfo.ReturnTypeGenericParameters)
                {
                    parameters.Add(new GenericParameterStringProcessor(genericParameter).GetDeclaration(genericParametersDelimiter));
                }
                return "<" + string.Join("," + genericParametersDelimiter, parameters) + ">";
            }
            else
            {
                return "";
            }
        }

        protected string CreatePropertyPrefixAccessModifier(AssemblyPropertyInfo propertyInfo)
        {
            if (!propertyInfo.HasGetter)
            {
                return CreatePropertyPrefixAccessModifier(propertyInfo);
            }
            else if (!propertyInfo.HasSetter)
            {
                return CreatePropertyGetterAccessModifier(propertyInfo);
            }
            else
            {
                string modifier = "";
                if (propertyInfo.IsGetterInternal && propertyInfo.IsSetterInternal)
                {
                    modifier = "internal";
                }
                if (propertyInfo.IsGetterPrivate && propertyInfo.IsSetterPrivate)
                {
                    modifier = "private";
                }
                if (propertyInfo.IsGetterPrivateProtected && propertyInfo.IsSetterPrivateProtected)
                {
                    modifier = "private protected";
                }
                if (propertyInfo.IsGetterProtected && propertyInfo.IsSetterProtected)
                {
                    modifier = "protected";
                }
                if (propertyInfo.IsGetterProtectedInternal && propertyInfo.IsSetterProtectedInternal)
                {
                    modifier = "protected internal";
                }
                if (propertyInfo.IsGetterPublic && propertyInfo.IsSetterPublic)
                {
                    modifier = "public";
                }

                return modifier;
            }
        }

        protected string CreatePropertyPostfixAccessModifier(AssemblyPropertyInfo propertyInfo)
        {
            string getter;
            if (propertyInfo.HasGetter)
            {
                getter = string.Format(" {0} get;", CreatePropertyGetterAccessModifier(propertyInfo));
            }
            else
            {
                getter = "";
            }

            string setter;
            if (propertyInfo.HasSetter)
            {
                setter = string.Format(" {0} set;", CreatePropertySetterAccessModifier(propertyInfo));
            }
            else
            {
                setter = "";
            }

            return "{" + getter + setter + " }";
        }

        protected string CreatePropertyGetterAccessModifier(AssemblyPropertyInfo propertyInfo)
        {
            if (!propertyInfo.HasGetter)
            {
                throw new NoSuchMethodException("No getter in " + propertyInfo.Name);
            }

            string modifier = "";
            if (propertyInfo.IsGetterInternal)
            {
                modifier = "internal";
            }
            if (propertyInfo.IsGetterPrivate)
            {
                modifier = "private";
            }
            if (propertyInfo.IsGetterPrivateProtected)
            {
                modifier = "private protected";
            }
            if (propertyInfo.IsGetterProtected)
            {
                modifier = "protected";
            }
            if (propertyInfo.IsGetterProtectedInternal)
            {
                modifier = "protected internal";
            }
            if (propertyInfo.IsGetterPublic)
            {
                modifier = "public";
            }

            return modifier;
        }

        protected string CreatePropertySetterAccessModifier(AssemblyPropertyInfo propertyInfo)
        {
            if (!propertyInfo.HasSetter)
            {
                throw new NoSuchMethodException("No setter in " + propertyInfo.Name);
            }

            string modifier = "";
            if (propertyInfo.IsSetterInternal)
            {
                modifier = "internal";
            }
            if (propertyInfo.IsSetterPrivate)
            {
                modifier = "private";
            }
            if (propertyInfo.IsSetterPrivateProtected)
            {
                modifier = "private protected";
            }
            if (propertyInfo.IsSetterProtected)
            {
                modifier = "protected";
            }
            if (propertyInfo.IsSetterProtectedInternal)
            {
                modifier = "protected internal";
            }
            if (propertyInfo.IsSetterPublic)
            {
                modifier = "public";
            }

            return modifier;
        }

        public PropertyStringProcessor(AssemblyPropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;
        }
    }
}
