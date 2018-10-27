using System.Collections.Generic;
using AssemblyBrowser.Model;

namespace AssemblyBrowser.ViewModel
{
    public class NamespaceStringProcessor
    {
        protected readonly AssemblyNamespaceInfo namespaceInfo;

        public IEnumerable<string> GetDatatypes(string modifiersDelimiter)
        {
            List<string> datatypesNames = new List<string>();
            foreach (AssemblyDatatypeInfo datatypeInfo in namespaceInfo.Datatypes)
            {
                datatypesNames.Add(new DatatypeStringProcessor(datatypeInfo).GetDeclaration(modifiersDelimiter));
            }
            return datatypesNames;
        }

        public IEnumerable<string> GetDatatypeFields(string datatype, string modifiersDelimiter)
        {
            AssemblyDatatypeInfo datatypeInfo = GetDatatypeByName(datatype);
            if (datatypeInfo == null)
            {
                throw new NoSuchDatatypeException("No datatype " + datatype);
            }
            return new DatatypeStringProcessor(datatypeInfo).GetFieldsDeclarations(modifiersDelimiter);
        }

        public IEnumerable<string> GetDatatypeProperties(string datatype, string modifiersDelimiter)
        {
            AssemblyDatatypeInfo datatypeInfo = GetDatatypeByName(datatype);
            if (datatypeInfo == null)
            {
                throw new NoSuchDatatypeException("No datatype " + datatype);
            }
            return new DatatypeStringProcessor(datatypeInfo).GetPropertiesDeclarations(modifiersDelimiter);
        }

        public IEnumerable<string> GetDatatypeMethods(string datatype, string modifiersDelimiter)
        {
            AssemblyDatatypeInfo datatypeInfo = GetDatatypeByName(datatype);
            if (datatypeInfo == null)
            {
                throw new NoSuchDatatypeException("No datatype " + datatype);
            }
            return new DatatypeStringProcessor(datatypeInfo).GetMethodsDeclarations(modifiersDelimiter);
        }

        protected AssemblyDatatypeInfo GetDatatypeByName(string name)
        {
            foreach (AssemblyDatatypeInfo datatypeInfo in namespaceInfo.Datatypes)
            {
                if (datatypeInfo.Name.Equals(name))
                {
                    return datatypeInfo;
                }
            }
            return null;
        }

        public NamespaceStringProcessor(AssemblyNamespaceInfo namespaceInfo)
        {
            this.namespaceInfo = namespaceInfo;
        }
    }
}
