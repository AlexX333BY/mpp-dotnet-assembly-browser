using System.Collections.Generic;

namespace AssemblyBrowser.Model
{
    public interface IReturnTypeGenericable
    {
        bool IsReturnTypeGeneric
        { get; }

        List<AssemblyGenericParameterInfo> ReturnTypeGenericParameters
        { get; }
    }
}
