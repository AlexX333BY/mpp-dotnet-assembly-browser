using System;

namespace AssemblyBrowser.ViewModel
{
    public class NoSuchDatatypeException : Exception
    {
        public NoSuchDatatypeException(string message) : base(message)
        { }
    }
}
