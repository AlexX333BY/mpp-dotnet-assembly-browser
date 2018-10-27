using System;

namespace AssemblyBrowser.ViewModel
{
    public class NoSuchMethodException : Exception
    {
        public NoSuchMethodException(string message) : base(message)
        { }
    }
}
