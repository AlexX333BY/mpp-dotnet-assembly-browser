using System.Collections.Generic;

namespace AssemblyBrowser.Model
{
    public interface IGenericParametrized
    {
        bool IsGenericParametrized
        { get; }

        List<AssemblyGenericParameterInfo> GenericParameters
        { get; }
    }
}
