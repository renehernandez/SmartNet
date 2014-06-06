using System;
using System.Collections.Generic;

namespace SmartNet.Interfaces
{
    public interface IGraph<TVertex, TEdge>
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TVertex>
    {

        # region Public Properties

        int NumberOfVertices { get; }

        int NumberOfEdges { get; }

        TVertex[] Vertices { get; }

        IEnumerable<TVertex> VerticesIterator { get; }

        TEdge[] Edges { get; }

        IEnumerable<TEdge> EdgesIterator { get; } 

        # endregion

        # region Public Methods

        void AddVertex(TVertex vertex);

        void AddVertices(IEnumerable<TVertex> vertices);

        void AddVertices(params TVertex[] vertices);

        void AddEdge(TEdge edge);

        void AddEdge(TVertex source, TVertex target);

        void AddEdge(Tuple<TVertex, TVertex> tuple);

        void AddEdges(IEnumerable<TEdge> edges);

        void AddEdges(params TEdge[] edges);

        void AddEdges(IEnumerable<Tuple<TVertex, TVertex>> edges);

        void AddEdges(params Tuple<TVertex, TVertex>[] edges);

        void AddPath(IEnumerable<TVertex> vertices);

        void AddPath(params TVertex[] vertices);

        void AddCycle(IEnumerable<TVertex> vertices);

        void AddCycle(params TVertex[] vertices);

        bool HasVertex(TVertex vertex);
        
        bool HasEdge(TEdge edge);

        IEnumerable<TVertex> NeighborsIterator(TVertex vertex);

        TVertex[] Neighbors(TVertex vertex);

        IGraph<TVertex, TEdge> Subgraph(IEnumerable<TVertex> vertices);

        IGraph<TVertex, TEdge> Subgraph(TVertex[] vertices);

        IEnumerable<IEnumerable<TVertex>> AdjacencyListIterator();

        TVertex[][] AdjacencyList();

        void Clear();

        void RemoveVertex(TVertex vertex);

        void RemoveVertices(IEnumerable<TVertex> vertices);

        void RemoveVertices(TVertex[] vertices);

        void RemoveEdge(TEdge edge);

        void RemoveEdges(IEnumerable<TEdge> edges);

        void RemoveEdges(TEdge[] edges);

        # endregion


    }
}
