using System.Collections.Generic;

namespace AssemblyBrowser.Model
{
    public interface IGenericParametrizable
    {
        bool IsGenericParametrized
        { get; }

        List<AssemblyGenericParameterInfo> GenericParameters
        { get; }
    }
}
