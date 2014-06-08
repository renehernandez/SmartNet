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
    public class Graph<TVertex, TEdge> : BaseGraph<TVertex, TEdge>, IGraph<Graph<TVertex, TEdge>, TVertex, TEdge>
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TEdge, TVertex>
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
            if (!adj.ContainsKey(edge.Source))
                AddVertex(edge.Source);
            if (!adj.ContainsKey(edge.Target))
                AddVertex(edge.Target);

            if (adj[edge.Source].ContainsKey(edge.Target))
                throw new DuplicatedEdgeException("Edge {0} already present in graph", edge);

            adj[edge.Source].Add(edge.Target, edge);
            adj[edge.Target].Add(edge.Source, edgeFactory(edge.Target, edge.Source));

            NumberOfEdges++;
        }

        public Graph<TVertex, TEdge> Subgraph(IEnumerable<TVertex> vertices)
        {
            var listVertices = vertices.ToList();
            var subgraph = new Graph<TVertex, TEdge>(listVertices);

            subgraph.AddEdges(
                listVertices.SelectMany(vertex => adj[vertex].Values.Where(
                    edge => subgraph.HasVertex(edge.Target) && !subgraph.HasEdge(vertex, edge.Target))
                )
                );

            return subgraph;
        }

        public Graph<TVertex, TEdge> Subgraph(params TVertex[] vertices)
        {
            return Subgraph(vertices.AsEnumerable());
        }

        public override void RemoveEdge(TVertex source, TVertex target)
        {
            if (!adj.ContainsKey(source))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge {1} <-> {2}", source, source, target);

            if (!adj.ContainsKey(target))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge {1} <-> {2}", source, source, target);

            if (!adj[source].ContainsKey(target))
                throw new EdgeNotFoundException("Edge {0} <-> {1} not found in graph", source, target);

            adj[source].Remove(target);
            adj[target].Remove(source);

            NumberOfEdges--;
        }

        # endregion

    }
}
