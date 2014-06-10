using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Factories
{
    public delegate TEdge EdgeFromVerticesFactory<in TVertex, out TEdge, TData>(TVertex source, TVertex target)
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TEdge, TVertex, TData> 
        where TData : IData, new();

    public delegate TEdge EdgeFromEdgesFactory<TVertex, TEdge, TData>(TEdge first, TEdge second)
        where TEdge : IEdge<TEdge, TVertex, TData>
        where TVertex : IEquatable<TVertex> 
        where TData : IData, new();

    public delegate TEdge ReverseEdgeFactory<TVertex, TEdge, TData>(TEdge edge)
        where TEdge : IEdge<TEdge, TVertex, TData>
        where TVertex : IEquatable<TVertex> 
        where TData : IData, new();
}
