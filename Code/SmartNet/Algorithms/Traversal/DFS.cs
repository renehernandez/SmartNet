using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Algorithms.Traversal
{
    public static class DFS
    {

        public static IEnumerable<T> Vertices<T>(Graph<T> graph) where T : IEquatable<T>
        {
            return Vertices(graph, graph.VerticesIterator.First());
        }

        public static IEnumerable<T> Vertices<T>(Graph<T> graph, T vertex) where T : IEquatable<T>
        {
            var mark = new HashSet<T> {vertex};


            foreach (var w in VerticesVisit(graph, vertex, mark))
                yield return w;

            foreach (var w in graph.VerticesIterator)
            {
                if (mark.Contains(w))
                    continue;

                mark.Add(w);

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }

        }

        private static IEnumerable<T> VerticesVisit<T>(Graph<T> graph, T vertex, HashSet<T> mark) where T : IEquatable<T>
        {
            foreach (var w in graph.NeighborsIterator(vertex))
            {
                if (mark.Contains(w))
                    continue;

                mark.Add(w);
                yield return w;

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }
        }

        public static IEnumerable<Edge<T>> Edges<T>(Graph<T> graph) where T: IEquatable<T>
        {
            return Edges(graph, graph.VerticesIterator.First());
        }

        public static IEnumerable<Edge<T>> Edges<T>(Graph<T> graph, T vertex) where T: IEquatable<T>
        {
            var mark = new HashSet<T> {vertex};

            foreach (var edge in EdgesVisit(graph, vertex, mark))
                yield return edge;

            foreach(var w in graph.VerticesIterator)
            {
                if (mark.Contains(w))
                    continue;

                mark.Add(w);
                foreach (var edge in EdgesVisit(graph, w, mark))
                    yield return edge;
            }
            
        }

        private static IEnumerable<Edge<T>> EdgesVisit<T>(Graph<T> graph, T v, HashSet<T> mark) 
            where T: IEquatable<T>
        {
            foreach (var keyValue in graph[v])
            {
                if (mark.Contains(keyValue.Key))
                    continue;

                mark.Add(keyValue.Key);
                yield return keyValue.Value;

                foreach (var edge in EdgesVisit(graph, keyValue.Key, mark))
                    yield return edge;
            }
        }

    }
}
