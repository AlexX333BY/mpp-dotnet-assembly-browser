using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
