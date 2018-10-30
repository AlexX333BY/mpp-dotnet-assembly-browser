using System.Collections.Generic;

namespace AssemblyBrowser.Model
{
    public interface IGenericReturn
    {
        bool IsReturnTypeGeneric
        { get; }

        List<AssemblyGenericParameterInfo> ReturnTypeGenericParameters
        { get; }
    }
}
