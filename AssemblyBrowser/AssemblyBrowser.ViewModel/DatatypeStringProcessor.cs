using System.Collections.Generic;
using System.IO;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class DatatypeStringProcessor : IDeclaredElementStringProcessor
    {
        protected readonly AssemblyDatatypeInfo datatypeInfo;

        public string GetDeclaration(string modifiersDelimiter)
        {
            List<string> modifiers = new List<string>
            {
                AssemblyStringProcessor.CreateAccessModifier(datatypeInfo)
            };

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
            modifiers.Add(datatypeInfo.Name);

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
