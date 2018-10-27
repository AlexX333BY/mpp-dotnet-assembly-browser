using System;

namespace AssemblyBrowser.ViewModel
{
    public class NoSuchNamespaceException : Exception
    {
        public NoSuchNamespaceException(string message) : base(message)
        { }
    }
}
