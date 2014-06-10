using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Exceptions;
using SmartNet.Factories;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class Graph<TVertex, TEdge, TData> : BaseGraph<TVertex, TEdge, TData>, IGraph<Graph<TVertex, TEdge, TData>, TVertex, TEdge, TData>
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TEdge, TVertex, TData> 
        where TData : IData, new()
    {

        # region Public Properties

        # endregion

        # region Constructors

        public Graph() :base()
        {
        }

        public Graph(IEnumerable<TVertex> vertices): base(vertices)
        {
        }

        public Graph(params TVertex[] vertices) : base(vertices)
        {
        }

        public Graph(IEnumerable<TEdge> edges) : base(edges)
        {
        }

        public Graph(params TEdge[] edges) : base(edges)
        {
        }

        public Graph(IEqualityComparer<TVertex> comparer) : base(comparer)
        {
        }

        public Graph(IEqualityComparer<TVertex> comparer, IEnumerable<TVertex> vertices)
            : base(comparer, vertices)
        {
        }

        public Graph(IEqualityComparer<TVertex> comparer, params TVertex[] vertices)
            : base(comparer, vertices)
        {
        }

        public Graph(IEqualityComparer<TVertex> comparer, IEnumerable<TEdge> edges)
            : base(comparer, edges)
        {
        }

        public Graph(IEqualityComparer<TVertex> comparer, params TEdge[] edges)
            : base(comparer, edges)
        {
        }

        # endregion

        # region Public Methods

        public override void AddEdge(TEdge edge)
        {
            if (!Adj.ContainsKey(edge.Source))
                AddVertex(edge.Source);
            if (!Adj.ContainsKey(edge.Target))
                AddVertex(edge.Target);

            if (Adj[edge.Source].ContainsKey(edge.Target))
                throw new DuplicatedEdgeException("Edge {0} already present in graph", edge);

            Adj[edge.Source].Add(edge.Target, edge);
            Adj[edge.Target].Add(edge.Source, EdgeVerticesFactory(edge.Target, edge.Source));

            NumberOfEdges++;
        }

        public Graph<TVertex, TEdge, TData> Subgraph(IEnumerable<TVertex> vertices)
        {
            var listVertices = vertices.ToList();
            var subgraph = new Graph<TVertex, TEdge, TData>(listVertices);

            subgraph.AddEdges(
                listVertices.SelectMany(vertex => Adj[vertex].Values.Where(
                    edge => subgraph.HasVertex(edge.Target) && !subgraph.HasEdge(vertex, edge.Target))
                )
                );

            return subgraph;
        }

        public Graph<TVertex, TEdge, TData> Subgraph(params TVertex[] vertices)
        {
            return Subgraph(vertices.AsEnumerable());
        }

        public override void RemoveEdge(TVertex source, TVertex target)
        {
            if (!Adj.ContainsKey(source))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge {1} <-> {2}", source, source, target);

            if (!Adj.ContainsKey(target))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge {1} <-> {2}", source, source, target);

            if (!Adj[source].ContainsKey(target))
                throw new EdgeNotFoundException("Edge {0} <-> {1} not found in graph", source, target);

            Adj[source].Remove(target);
            Adj[target].Remove(source);

            NumberOfEdges--;
        }

        # endregion

    }
}
