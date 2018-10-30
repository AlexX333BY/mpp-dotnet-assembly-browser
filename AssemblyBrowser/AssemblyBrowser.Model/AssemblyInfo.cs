using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AssemblyBrowser.Model
{
    public class AssemblyInfo
    {
        protected readonly Dictionary<string, AssemblyNamespaceInfo> namespaces;

        protected Dictionary<Type, List<MethodInfo>> CollectExtensionMethods(Type[] types)
        {
            var result = new Dictionary<Type, List<MethodInfo>>();
            Type extendedType;
            foreach (Type type in types)
            {
                if (type.IsDefined(typeof(ExtensionAttribute), true))
                {
                    foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        if (method.IsDefined(typeof(ExtensionAttribute), true))
                        {
                            try
                            {
                                extendedType = method.GetParameters()[0].ParameterType;
                                if (!result.TryGetValue(extendedType, out List<MethodInfo> methods))
                                {
                                    methods = new List<MethodInfo>();
                                    result[extendedType] = methods;
                                }
                                methods.Add(method);
                            }
                            catch (FileNotFoundException)
                            {
                                // handle dependency file load error
                            }
                        }
                    }
                }
            }
            return result;
        }

        public Dictionary<string, AssemblyNamespaceInfo> Namespaces => new Dictionary<string, AssemblyNamespaceInfo>(namespaces);

        public AssemblyInfo(string assemblyPath)
        {
            namespaces = new Dictionary<string, AssemblyNamespaceInfo>();

            Assembly assembly = Assembly.LoadFile(assemblyPath);
            string namespaceName;

            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                List<Type> typeList = new List<Type>();
                foreach (Type type in e.Types)
                {
                    if (type != null)
                    {
                        typeList.Add(type);
                    }
                }
                types = typeList.ToArray();
            }
            Dictionary<Type, List<MethodInfo>> extensionMethods = CollectExtensionMethods(types);
            foreach (Type type in types)
            {
                try
                {
                    namespaceName = type.Namespace;
                    if (namespaceName != null)
                    {
                        if (!namespaces.TryGetValue(namespaceName, out AssemblyNamespaceInfo assemblyNamespace))
                        {
                            assemblyNamespace = new AssemblyNamespaceInfo(namespaceName);
                            namespaces.Add(namespaceName, assemblyNamespace);
                        }
                        if (!extensionMethods.TryGetValue(type, out List<MethodInfo> typeExtensionMethods))
                        {
                            typeExtensionMethods = null;
                        }
                        assemblyNamespace.AddDatatype(type, typeExtensionMethods);
                    }
                }
                catch (FileNotFoundException)
                {
                    // handle dependency file load error
                }
            }
        }
    }
}
