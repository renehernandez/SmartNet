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
    public class DiGraph<TVertex, TEdge, TData> : BaseGraph<TVertex, TEdge, TData>, IGraph<DiGraph<TVertex, TEdge, TData>, TVertex, TEdge, TData> 
        where TEdge : IEdge<TEdge, TVertex, TData> 
        where TVertex : IEquatable<TVertex> 
        where TData : IData, new()
    {

        # region Constructors

        public DiGraph() :base()
        {
        }

        public DiGraph(IEnumerable<TVertex> vertices): base(vertices)
        {
        }

        public DiGraph(params TVertex[] vertices) : base(vertices)
        {
        }

        public DiGraph(IEnumerable<TEdge> edges) : base(edges)
        {
        }

        public DiGraph(params TEdge[] edges) : base(edges)
        {
        }

        public DiGraph(IEqualityComparer<TVertex> comparer) : base(comparer)
        {
        }

        public DiGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TVertex> vertices)
            : base(comparer, vertices)
        {
        }

        public DiGraph(IEqualityComparer<TVertex> comparer, params TVertex[] vertices)
            : base(comparer, vertices)
        {
        }

        public DiGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TEdge> edges)
            : base(comparer, edges)
        {
        }

        public DiGraph(IEqualityComparer<TVertex> comparer, params TEdge[] edges)
            : base(comparer, edges)
        {
        }

        # endregion

        # region Public Methods

            # region Addition Methods

        public override void AddEdge(TEdge edge)
        {
            if (!Adj.ContainsKey(edge.Source))
                AddVertex(edge.Source);
            if (!Adj.ContainsKey(edge.Target))
                AddVertex(edge.Target);

            if (Adj[edge.Source].ContainsKey(edge.Target))
                throw new DuplicatedEdgeException("Edge {0} already present in graph", edge);

            Adj[edge.Source].Add(edge.Target, edge);

            NumberOfEdges++;
        }

            # endregion

            # region Subgraph Methods

        public DiGraph<TVertex, TEdge, TData> Subgraph(IEnumerable<TVertex> vertices)
        {
            var listVertices = vertices.ToList();
            var subgraph = new DiGraph<TVertex, TEdge, TData>(listVertices);

            subgraph.AddEdges(
                listVertices.SelectMany(vertex => Adj[vertex].Values.Where(
                    edge => subgraph.HasVertex(edge.Target))
                )
                );

            return subgraph;
        }

        public DiGraph<TVertex, TEdge, TData> Subgraph(params TVertex[] vertices)
        {
            return Subgraph(vertices.AsEnumerable());
        }

            # endregion

            # region Deletion Methods

        public override void RemoveEdge(TVertex source, TVertex target)
        {
            if (!Adj.ContainsKey(source))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge {1} -> {2}", source, source, target);

            if (!Adj.ContainsKey(target))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge {1} -> {2}", source, source, target);

            if (!Adj[source].ContainsKey(target))
                throw new EdgeNotFoundException("Edge {0} -> {1} not found in graph", source, target);

            Adj[source].Remove(target);

            NumberOfEdges--;
        }

            # endregion

        public Graph<TVertex, TEdge, TData> ToUndirected(bool reciprocal = false)
        {
            return ToUndirected((e1, e2) => e1.Data.Weight >= e2.Data.Weight ? e1 : e2, reciprocal);
        }

        public Graph<TVertex, TEdge, TData> ToUndirected(EdgeFromEdgesFactory<TVertex, TEdge, TData> factory, bool reciprocal = false)
        {
            var graph = new Graph<TVertex, TEdge, TData>();

            foreach (var pair in AdjacencyIterator())
            {
                foreach (var vertexEdge in pair.Value)
                {
                    if (HasEdge(vertexEdge.Key, pair.Key))
                    {
                        var edge = factory(this[vertexEdge.Key, pair.Key], vertexEdge.Value);
                        if(!graph.HasEdge(edge))
                            graph.AddEdge(edge);
                    }
                    else if(!reciprocal)
                        graph.AddEdge(vertexEdge.Value);
                }
            }

            return graph;
        } 

        public DiGraph<TVertex, TEdge, TData> Reverse(bool copy = true)
        {
            var edges = Edges;
            
            DiGraph<TVertex, TEdge, TData> graph;

            if (copy)
            {
                graph = new DiGraph<TVertex, TEdge, TData>();
            }
            else
            {
                graph = this;
                graph.RemoveEdges(edges);
            }

            foreach (var edge in edges)
            {
                graph.AddEdge(ReverseEdgeFactory(edge));
            }

            return graph;
        } 

        # endregion
    }
}
