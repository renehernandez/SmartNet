using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Interfaces
{
    public interface IEdge<TEdge, out TVertex, out TData> : IEquatable<TEdge> 
        where TEdge : IEdge<TEdge, TVertex, TData> 
        where TVertex: IEquatable<TVertex>
        where TData : IData, new()
    {

        TVertex Source { get; }

        TVertex Target { get; }

        TData Data { get; }

        TVertex this[int index] { get; }

    }
}
