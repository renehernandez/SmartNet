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
    public class DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData> : AbstractGraph<TVertex, TDiEdge, TGraphData, TEdgeData>, 
        IGraph<DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData>, TVertex, TDiEdge, TGraphData, TEdgeData>
        where TGraphData : IGraphData, new()
        where TDiEdge : DiEdge<TVertex, TEdgeData>, IEdge<TDiEdge, TVertex, TEdgeData> 
        where TVertex : IEquatable<TVertex> 
        where TEdgeData : IEdgeData, new() 
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

        public DiGraph(IEnumerable<TDiEdge> edges) : base(edges)
        {
        }

        public DiGraph(params TDiEdge[] edges) : base(edges)
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

        public DiGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TDiEdge> edges)
            : base(comparer, edges)
        {
        }

        public DiGraph(IEqualityComparer<TVertex> comparer, params TDiEdge[] edges)
            : base(comparer, edges)
        {
        }

        # endregion

        # region Public Methods

            # region Addition Methods

        public override void AddEdge(TDiEdge edge)
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

        public virtual DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData> Subgraph(IEnumerable<TVertex> vertices)
        {
            var listVertices = vertices.ToList();
            var subgraph = new DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData>(listVertices);

            subgraph.AddEdges(
                listVertices.SelectMany(vertex => Adj[vertex].Values.Where(
                    edge => subgraph.HasVertex(edge.Target))
                )
                );

            return subgraph;
        }

        public virtual DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData> Subgraph(params TVertex[] vertices)
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

        public virtual Graph<TVertex, Edge<TVertex, TEdgeData>, TGraphData, TEdgeData> ToUndirected(bool reciprocal = false) 
        {
            return ToUndirected((e1, e2) =>
            {
                var edge = e1.Data.Weight >= e2.Data.Weight ? e1 : e2;
                return new Edge<TVertex, TEdgeData>(edge.Source, edge.Target, edge.Data);
            }, edge =>  new Edge<TVertex, TEdgeData>(edge.Source, edge.Target, edge.Data),reciprocal);
        }

        public virtual Graph<TVertex, TEdge, TGraphData, TEdgeData> ToUndirected<TEdge>(EdgeFromDiEdgesFactory<TVertex, TDiEdge, TEdgeData, TEdge> fromDiEdges, EdgeFromDiEdgeFactory<TVertex, TDiEdge, TEdgeData, TEdge> edgeConverter,  bool reciprocal = false)
            where TEdge : Edge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData> 
        {
            var graph = new Graph<TVertex, TEdge, TGraphData, TEdgeData>();

            foreach (var pair in AdjacencyIterator())
            {
                foreach (var vertexEdge in pair.Value)
                {
                    if (HasEdge(vertexEdge.Key, pair.Key))
                    {
                        var edge = fromDiEdges(this[vertexEdge.Key, pair.Key], vertexEdge.Value);
                        if(!graph.HasEdge(edge))
                            graph.AddEdge(edge);
                    }
                    else if(!reciprocal)
                        graph.AddEdge(edgeConverter(vertexEdge.Value));
                }
            }

            return graph;
        } 

        public virtual DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData> Reverse(bool copy = true)
        {
            var edges = Edges;
            
            DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData> graph;

            if (copy)
            {
                graph = new DiGraph<TVertex, TDiEdge, TGraphData, TEdgeData>();
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
