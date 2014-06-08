using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Interfaces
{
    public interface IEdge<TEdge, out TVertex> : IEquatable<TEdge> 
        where TEdge : IEdge<TEdge, TVertex> 
        where TVertex: IEquatable<TVertex>
    {

        TVertex Source { get; }

        TVertex Target { get; }

        double Weight { get; set; }

        TVertex this[int index] { get; }

    }
}
