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

        public static IEnumerable<TVertex> Vertices<TVertex, TEdge>(Graph<TVertex, TEdge> graph) 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Vertices(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<TVertex> Vertices<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex) 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Vertices(graph, vertex, false);
        }

        private static IEnumerable<TVertex> Vertices<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex, bool complete)
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

        private static IEnumerable<TVertex> VerticesVisit<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex, HashSet<TVertex> mark) 
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

        public static IEnumerable<TEdge> Edges<TVertex, TEdge>(Graph<TVertex, TEdge> graph)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Edges(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<TEdge> Edges<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Edges(graph, vertex, false);
        } 

        private static IEnumerable<TEdge> Edges<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex, bool complete)
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

        private static IEnumerable<TEdge> EdgesVisit<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex v, HashSet<TVertex> mark)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            foreach (var keyValue in graph[v].Where(keyValue => !mark.Contains(keyValue.Key)))
            {
                mark.Add(keyValue.Key);
                yield return keyValue.Value;

                foreach (var edge in EdgesVisit(graph, keyValue.Key, mark))
                    yield return edge;
            }
        }

        public static Graph<TVertex, TEdge> Tree<TVertex, TEdge>(Graph<TVertex, TEdge> graph)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Tree(graph, graph.VerticesIterator.First(), true);
        }

        public static Graph<TVertex, TEdge> Tree<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Tree(graph, vertex, false);
        }

        private static Graph<TVertex, TEdge> Tree<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex, bool complete)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            var treeGraph = new Graph<TVertex, TEdge>();

            treeGraph.AddEdges(Edges(graph, vertex, complete));

            return treeGraph;
        }

    }

}
