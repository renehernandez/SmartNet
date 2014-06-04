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

        private static IEnumerable<T> Vertices<T>(Graph<T> graph, T vertex, bool complete) where T : IEquatable<T>
        {
            var mark = new HashSet<T> {vertex};


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

        private static IEnumerable<TVertex> VerticesVisit<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex, Dictionary<TVertex, bool> mark) 
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

        private static IEnumerable<Edge<T>> Edges<T>(Graph<T> graph, T vertex, bool complete) where T: IEquatable<T>
        {
            var mark = new HashSet<T> {vertex};

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

        private static IEnumerable<TEdge> EdgesVisit<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex v, Dictionary<TVertex, bool> mark)
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

        public static Graph<T> Tree<T>(Graph<T> graph) where T : IEquatable<T>
        {
            return Tree(graph, graph.VerticesIterator.First(), true);
        }

        public static Graph<T> Tree<T>(Graph<T> graph, T vertex) where T : IEquatable<T>
        {
            return Tree(graph, vertex, false);
        }

        private static Graph<T> Tree<T>(Graph<T> graph, T vertex, bool complete) where T : IEquatable<T>
        {
            var treeGraph = new Graph<T>();

            treeGraph.AddEdges(Edges(graph, vertex, complete));

            return treeGraph;
        }

    }

}
