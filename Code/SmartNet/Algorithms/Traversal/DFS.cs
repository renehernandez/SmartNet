using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Algorithms.Traversal
{
    public static class DFS
    {

        # region Vertices Methods

        public static IEnumerable<TVertex> Vertices<TVertex, TEdge, TGraphData, TEdgeData>(Graph<TVertex, TEdge, TGraphData, TEdgeData> graph)
            where TVertex : IEquatable<TVertex>
            where TEdge : Edge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData> 
            where TEdgeData : IEdgeData, new() 
            where TGraphData : IGraphData, new()
        {
            return Vertices(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<TVertex> Vertices<TVertex, TEdge, TGraphData, TEdgeData>(DiGraph<TVertex, TEdge, TGraphData, TEdgeData> graph)
            where TVertex : IEquatable<TVertex>
            where TEdge : DiEdge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            return Vertices(graph, graph.VerticesIterator.First(), true);
        }


        public static IEnumerable<TVertex> Vertices<TVertex, TEdge, TGraphData, TEdgeData>(Graph<TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : Edge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            return Vertices(graph, vertex, false);
        }

        public static IEnumerable<TVertex> Vertices<TVertex, TEdge, TGraphData, TEdgeData>(DiGraph<TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : DiEdge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            return Vertices(graph, vertex, false);
        }

        private static IEnumerable<TVertex> Vertices<TGraph, TVertex, TEdge, TGraphData, TEdgeData>(IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex, bool complete)
            where TGraph : IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData>, new()
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            var mark = new HashSet<TVertex> {vertex};

            foreach (var w in VerticesVisit(graph, vertex, mark))
                yield return w;

            if (!complete) 
                yield break;
            
            foreach (var w in graph.VerticesIterator.Where(w => !mark.Contains(w)))
            {
                mark.Add(w);

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }
        }

        private static IEnumerable<TVertex> VerticesVisit<TGraph, TVertex, TEdge, TGraphData, TEdgeData>(IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex, HashSet<TVertex> mark)
            where TGraph : IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData>, new()
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            foreach (var w in graph.NeighborsIterator(vertex).Where(w => !mark.Contains(w)))
            {
                mark.Add(w);
                yield return w;

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }
        }

        # endregion

        # region Edges Methods

        public static IEnumerable<TEdge> Edges<TVertex, TEdge, TGraphData, TEdgeData>(Graph<TVertex, TEdge, TGraphData, TEdgeData> graph)
            where TVertex : IEquatable<TVertex>
            where TEdge : Edge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            return Edges(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<TEdge> Edges<TVertex, TEdge, TGraphData, TEdgeData>(DiGraph<TVertex, TEdge, TGraphData, TEdgeData> graph)
            where TVertex : IEquatable<TVertex>
            where TEdge : DiEdge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            return Edges(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<TEdge> Edges<TVertex, TEdge, TGraphData, TEdgeData>(DiGraph<TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : DiEdge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            return Edges(graph, vertex, false);
        }

        public static IEnumerable<TEdge> Edges<TVertex, TEdge, TGraphData, TEdgeData>(Graph<TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : Edge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            return Edges(graph, vertex, false);
        }

        private static IEnumerable<TEdge> Edges<TGraph, TVertex, TEdge, TGraphData, TEdgeData>(IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex, bool complete)
            where TGraph : IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData>, new()
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            var mark = new HashSet<TVertex> {vertex};

            foreach (var edge in EdgesVisit(graph, vertex, mark))
                yield return edge;

            if (!complete)
                yield break;

            foreach (var w in graph.VerticesIterator.Where(w => !mark.Contains(w)))
            {
                mark.Add(w);
                foreach (var edge in EdgesVisit(graph, w, mark))
                    yield return edge;
            }
            
        }

        private static IEnumerable<TEdge> EdgesVisit<TGraph, TVertex, TEdge, TGraphData, TEdgeData>(IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex, HashSet<TVertex> mark)
            where TGraph : IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData>, new()
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            foreach (var neighborEdge in graph.NeighborsEdgesIterator(vertex).Where(edge => !mark.Contains(edge.Target)))
            {
                mark.Add(neighborEdge.Target);
                yield return neighborEdge;

                foreach (var edge in EdgesVisit(graph, neighborEdge.Target, mark))
                    yield return edge;
            }
        }

        # endregion

        # region Tree Methods

        public static DiGraph<TVertex, TEdge, TGraphData, TEdgeData> Tree<TVertex, TEdge, TGraphData, TEdgeData>(DiGraph<TVertex, TEdge, TGraphData, TEdgeData> graph)
            where TVertex : IEquatable<TVertex>
            where TGraphData : IGraphData, new()
            where TEdge : DiEdge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
        {
            return Tree(graph, graph.VerticesIterator.First(), true);
        }

        public static DiGraph<TVertex, TEdge, TGraphData, TEdgeData> Tree<TVertex, TEdge, TGraphData, TEdgeData>(DiGraph<TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : DiEdge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            return Tree(graph, vertex, false);
        }

        public static Graph<TVertex, TEdge, TGraphData, TEdgeData> Tree<TVertex, TEdge, TGraphData, TEdgeData>(Graph<TVertex, TEdge, TGraphData, TEdgeData> graph)
            where TVertex : IEquatable<TVertex>
            where TGraphData : IGraphData, new() 
            where TEdge : Edge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData> 
            where TEdgeData : IEdgeData, new()
        {
            return Tree(graph, graph.VerticesIterator.First(), true);
        }

        public static Graph<TVertex, TEdge, TGraphData, TEdgeData> Tree<TVertex, TEdge, TGraphData, TEdgeData>(Graph<TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : Edge<TVertex, TEdgeData>, IEdge<TEdge, TVertex, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            return Tree(graph, vertex, false);
        }

        private static TGraph Tree<TGraph, TVertex, TEdge, TGraphData, TEdgeData>(IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData> graph, TVertex vertex, bool complete)
            where TGraph : IGraph<TGraph, TVertex, TEdge, TGraphData, TEdgeData>, new()
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TEdge, TVertex, TEdgeData>
            where TEdgeData : IEdgeData, new()
            where TGraphData : IGraphData, new()
        {
            var treeGraph = graph.Subgraph();

            treeGraph.AddEdges(Edges(graph, vertex, complete));

            return treeGraph;
        }
        # endregion

    }

}
