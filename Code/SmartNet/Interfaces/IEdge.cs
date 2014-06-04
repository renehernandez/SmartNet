using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Interfaces
{
    public interface IEdge<T> : IEquatable<IEdge<T>> where T: IEquatable<T>
    {

        T Source { get; }

        T Target { get; }

        double Weight { get; set; }

    }
}
