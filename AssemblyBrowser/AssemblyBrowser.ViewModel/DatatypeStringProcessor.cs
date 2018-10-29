using System;
using System.Collections.Generic;
using System.IO;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class DatatypeStringProcessor : IDeclaredElementStringProcessor
    {
        protected readonly AssemblyDatatypeInfo datatypeInfo;

        protected string GetGenericParametersDeclaration(string modifiersDelimiter)
        {
            if (datatypeInfo.IsGeneric)
            {
                List<string> parameters = new List<string>();
                foreach (AssemblyGenericParameterInfo genericParameter in datatypeInfo.GenericParameters)
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
                AssemblyStringProcessor.CreateAccessModifier(datatypeInfo)
            };

            if (datatypeInfo.IsClass)
            {
                if (datatypeInfo.IsStatic)
                {
                    modifiers.Add("static");
                }
                if (datatypeInfo.IsAbstract)
                {
                    modifiers.Add("abstract");
                }
                if (datatypeInfo.IsSealed)
                {
                    modifiers.Add("sealed");
                }
                modifiers.Add("class");
            }
            if (datatypeInfo.IsInterface)
            {
                modifiers.Add("interface");
            }
            if (datatypeInfo.IsStruct)
            {
                modifiers.Add("struct");
            }
            if (datatypeInfo.IsEnum)
            {
                modifiers.Add("enum");
            }

            modifiers.Add(datatypeInfo.Name + GetGenericParametersDeclaration(modifiersDelimiter));

            if (datatypeInfo.IsClass)
            {
                AssemblyDatatypeInfo baseType = datatypeInfo.BaseType;
                List<AssemblyDatatypeInfo> interfaces = datatypeInfo.Interfaces;
                if (!baseType.Name.Equals(nameof(Object)) || (interfaces.Count != 0))
                {
                    modifiers.Add(":");
                }

                if (!baseType.Name.Equals(nameof(Object)))
                {
                    if (interfaces.Count == 0)
                    {
                        modifiers.Add(baseType.Name);
                    }
                    else
                    {
                        modifiers.Add(baseType.Name + ",");
                    }
                }

                for (int i = 0; i < interfaces.Count - 1; i++)
                {
                    modifiers.Add(interfaces[i].Name + ",");
                }
                if (interfaces.Count != 0)
                {
                    modifiers.Add(interfaces[interfaces.Count - 1].Name);
                }
            }

            if (datatypeInfo.IsInterface)
            {
                List<AssemblyDatatypeInfo> interfaces = datatypeInfo.Interfaces;
                if (interfaces.Count != 0)
                {
                    modifiers.Add(":");
                }

                for (int i = 0; i < interfaces.Count - 1; i++)
                {
                    modifiers.Add(interfaces[i].Name + ",");
                }
                if (interfaces.Count != 0)
                {
                    modifiers.Add(interfaces[interfaces.Count - 1].Name);
                }
            }
            return string.Join(modifiersDelimiter, modifiers);
        }

        public IEnumerable<string> GetFieldsDeclarations(string modifiersDelimiter)
        {
            List<string> fields = new List<string>();
            foreach (AssemblyFieldInfo fieldInfo in datatypeInfo.Fields)
            {
                try
                {
                    fields.Add(new FieldStringProcessor(fieldInfo).GetDeclaration(modifiersDelimiter));
                }
                catch (FileNotFoundException)
                {
                    // handling error of loading non-available types
                }
            }
            return fields;
        }

        public IEnumerable<string> GetPropertiesDeclarations(string modifiersDelimiter)
        {
            List<string> properties = new List<string>();
            foreach (AssemblyPropertyInfo propertyInfo in datatypeInfo.Properties)
            {
                try
                {
                    properties.Add(new PropertyStringProcessor(propertyInfo).GetDeclaration(modifiersDelimiter));
                }
                catch (FileNotFoundException)
                {
                    // handling error of loading non-available types
                }
            }
            return properties;
        }

        public IEnumerable<string> GetMethodsDeclarations(string modifiersDelimiter)
        {
            List<string> methods = new List<string>();
            foreach (AssemblyMethodInfo methodInfo in datatypeInfo.Methods)
            {
                try
                {
                    methods.Add(new MethodStringProcessor(methodInfo).GetDeclaration(modifiersDelimiter));
                }
                catch (FileNotFoundException)
                {
                    // handling error of loading non-available types
                }
            }
            return methods;
        }

        public DatatypeStringProcessor(AssemblyDatatypeInfo datatypeInfo)
        {
            this.datatypeInfo = datatypeInfo;
        }
    }
}
