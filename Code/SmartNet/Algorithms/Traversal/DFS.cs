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

        public static IEnumerable<TVertex> Vertices<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph)
            where TGraph: IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return VerticesSolver(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<TVertex> Vertices<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex)
            where TGraph : IGraph<TGraph, TVertex, TEdge>
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return VerticesSolver(graph, vertex, false);
        }

        private static IEnumerable<TVertex> VerticesSolver<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex, bool complete)
            where TGraph : IGraph<TGraph, TVertex, TEdge>
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
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

        private static IEnumerable<TVertex> VerticesVisit<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex, HashSet<TVertex> mark)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            foreach (var w in graph.NeighborsIterator(vertex).Where(w => !mark.Contains(w)))
            {
                mark.Add(w);
                yield return w;

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }
        }

        public static IEnumerable<TEdge> Edges<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Edges(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<TEdge> Edges<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Edges(graph, vertex, false);
        }

        private static IEnumerable<TEdge> Edges<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex, bool complete)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
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

        private static IEnumerable<TEdge> EdgesVisit<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex, HashSet<TVertex> mark)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            foreach (var neighborEdge in graph.NeighborsEdgesIterator(vertex).Where(edge => !mark.Contains(edge.Target)))
            {
                mark.Add(neighborEdge.Target);
                yield return neighborEdge;

                foreach (var edge in EdgesVisit(graph, neighborEdge.Target, mark))
                    yield return edge;
            }
        }

        public static TGraph Tree<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Tree(graph, graph.VerticesIterator.First(), true);
        }

        public static TGraph Tree<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Tree(graph, vertex, false);
        }

        private static TGraph Tree<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex, bool complete)
            where TGraph : IGraph<TGraph, TVertex, TEdge> 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            var treeGraph = graph.Subgraph();

            treeGraph.AddEdges(Edges(graph, vertex, complete));

            return treeGraph;
        }

    }

}
