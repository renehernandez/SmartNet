using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Factories
{
    public delegate TEdge EdgeFromVerticesFactory<in TVertex, TData, out TEdge>(TVertex source, TVertex target)
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TEdge, TVertex, TData> 
        where TData : IEdgeData, new();

    public delegate TEdge EdgeFromDiEdgesFactory<TVertex, in TDiEdge, TData, out TEdge>(TDiEdge first, TDiEdge second)
        where TEdge : Edge<TVertex, TData>, IEdge<TEdge, TVertex, TData>
        where TDiEdge : DiEdge<TVertex, TData>, IEdge<TDiEdge, TVertex, TData>
        where TVertex : IEquatable<TVertex> 
        where TData : IEdgeData, new();

    public delegate TEdge ReverseEdgeFactory<TVertex, TData, TEdge>(TEdge edge)
        where TEdge : IEdge<TEdge, TVertex, TData>
        where TVertex : IEquatable<TVertex> 
        where TData : IEdgeData, new();

    public delegate TEdge EdgeFromDiEdgeFactory<TVertex, in TDiEdge, TData, out TEdge>(TDiEdge edge)
        where TEdge : Edge<TVertex, TData>, IEdge<TEdge, TVertex, TData>
        where TDiEdge : DiEdge<TVertex, TData>, IEdge<TDiEdge, TVertex, TData>
        where TVertex : IEquatable<TVertex>
        where TData : IEdgeData, new();
}
