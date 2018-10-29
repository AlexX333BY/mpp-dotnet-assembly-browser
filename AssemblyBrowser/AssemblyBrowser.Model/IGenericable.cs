using System.Collections.Generic;

namespace AssemblyBrowser.Model
{
    public interface IGenericable
    {
        bool IsGeneric
        { get; }

        List<AssemblyGenericParameterInfo> GenericParameters
        { get; }
    }
}
