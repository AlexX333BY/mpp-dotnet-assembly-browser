using AssemblyBrowser.Model;
using System.Collections.Generic;

namespace AssemblyBrowser.ViewModel
{
    public class AssemblyStringProcessor
    {
        protected const string defaultDelimiter = " ";
        protected readonly AssemblyInfo assemblyInfo;

        public IEnumerable<string> GetNamespaces()
        {
            return assemblyInfo.Namespaces.Keys;
        }

        public IEnumerable<string> GetNamespaceDatatypes(string namespaceName, string modifiersDelimiter = defaultDelimiter)
        {
            if (assemblyInfo.Namespaces.TryGetValue(namespaceName, out AssemblyNamespaceInfo namespaceInfo))
            {
                return new NamespaceStringProcessor(namespaceInfo).GetDatatypes(defaultDelimiter);
            }
            else
            {
                throw new NoSuchNamespaceException("No namespace " + namespaceName);
            }
        }

        public IEnumerable<string> GetDatatypeFields(string namespaceName, string datatypeName, string modifiersDelimiter = defaultDelimiter)
        {
            if (assemblyInfo.Namespaces.TryGetValue(namespaceName, out AssemblyNamespaceInfo namespaceInfo))
            {
                return new NamespaceStringProcessor(namespaceInfo).GetDatatypeFields(datatypeName, defaultDelimiter);
            }
            else
            {
                throw new NoSuchNamespaceException("No namespace " + namespaceName);
            }
        }

        public IEnumerable<string> GetDatatypeProperties(string namespaceName, string datatypeName, string modifiersDelimiter = defaultDelimiter)
        {
            if (assemblyInfo.Namespaces.TryGetValue(namespaceName, out AssemblyNamespaceInfo namespaceInfo))
            {
                return new NamespaceStringProcessor(namespaceInfo).GetDatatypeProperties(datatypeName, defaultDelimiter);
            }
            else
            {
                throw new NoSuchNamespaceException("No namespace " + namespaceName);
            }
        }

        public IEnumerable<string> GetDatatypeMethods(string namespaceName, string datatypeName, string modifiersDelimiter = defaultDelimiter)
        {
            if (assemblyInfo.Namespaces.TryGetValue(namespaceName, out AssemblyNamespaceInfo namespaceInfo))
            {
                return new NamespaceStringProcessor(namespaceInfo).GetDatatypeMethods(datatypeName, defaultDelimiter);
            }
            else
            {
                throw new NoSuchNamespaceException("No namespace " + namespaceName);
            }
        }

        protected internal static string CreateAccessModifier(IAccessable accessable)
        {
            string modifier = "";

            if (accessable.IsInternal)
            {
                modifier = "internal";
            }
            if (accessable.IsPrivate)
            {
                modifier = "private";
            }
            if (accessable.IsPrivateProtected)
            {
                modifier = "private protected";
            }
            if (accessable.IsProtected)
            {
                modifier = "protected";
            }
            if (accessable.IsProtectedInternal)
            {
                modifier = "protected internal";
            }
            if (accessable.IsPublic)
            {
                modifier = "public";
            }

            return modifier;
        }

        public AssemblyStringProcessor(string path)
        {
            assemblyInfo = new AssemblyInfo(path);
        }
    }
}
