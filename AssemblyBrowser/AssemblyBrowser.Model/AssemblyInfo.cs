using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser.Model
{
    public class AssemblyInfo
    {
        protected readonly Dictionary<string, AssemblyNamespaceInfo> namespaces;

        public Dictionary<string, AssemblyNamespaceInfo> Namespaces => new Dictionary<string, AssemblyNamespaceInfo>(namespaces);

        public AssemblyInfo(string assemblyPath)
        {
            namespaces = new Dictionary<string, AssemblyNamespaceInfo>();

            Assembly assembly = Assembly.LoadFile(assemblyPath);
            string namespaceName;

            foreach (Type type in assembly.GetTypes())
            {
                namespaceName = type.Namespace;
                if (namespaceName != null)
                {
                    if (!namespaces.TryGetValue(namespaceName, out AssemblyNamespaceInfo assemblyNamespace))
                    {
                        assemblyNamespace = new AssemblyNamespaceInfo(namespaceName);
                        namespaces.Add(namespaceName, assemblyNamespace);
                    }
                    assemblyNamespace.AddDatatype(type);
                }
            }
        }
    }
}
