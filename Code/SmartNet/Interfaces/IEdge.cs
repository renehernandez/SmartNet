using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Interfaces
{
    public interface IEdge<TVertex> : IEquatable<IEdge<TVertex>> 
        where TVertex: IEquatable<TVertex>
    {

        TVertex Source { get; }

        TVertex Target { get; }

        double Weight { get; set; }

        TVertex this[int index] { get; }

    }
}
