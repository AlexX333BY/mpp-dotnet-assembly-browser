using System;
using System.Collections.Generic;

namespace AssemblyBrowser.Model
{
    public class AssemblyNamespaceInfo
    {
        protected readonly List<AssemblyDatatypeInfo> datatypes;

        public string Name
        { get; protected set; }

        public List<AssemblyDatatypeInfo> Datatypes => new List<AssemblyDatatypeInfo>(datatypes);

        protected internal void AddDatatype(Type datatype)
        {
            datatypes.Add(new AssemblyDatatypeInfo(datatype));
        }

        public AssemblyNamespaceInfo(string name)
        {
            Name = name;
            datatypes = new List<AssemblyDatatypeInfo>();
        }
    }
}
