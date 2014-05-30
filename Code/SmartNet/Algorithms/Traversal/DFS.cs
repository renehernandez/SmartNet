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
            return Vertices(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<T> Vertices<T>(Graph<T> graph, T vertex) where T : IEquatable<T>
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



        private static IEnumerable<T> VerticesVisit<T>(Graph<T> graph, T vertex, HashSet<T> mark) where T : IEquatable<T>
        {
            foreach (var w in graph.NeighborsIterator(vertex).Where(w => !mark.Contains(w)))
            {
                mark.Add(w);
                yield return w;

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }
        }

        public static IEnumerable<Edge<T>> Edges<T>(Graph<T> graph) where T: IEquatable<T>
        {
            return Edges(graph, graph.VerticesIterator.First(), true);
        }

        public static IEnumerable<Edge<T>> Edges<T>(Graph<T> graph, T vertex) where T: IEquatable<T>
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

        private static IEnumerable<Edge<T>> EdgesVisit<T>(Graph<T> graph, T v, HashSet<T> mark) 
            where T: IEquatable<T>
        {
            foreach (var keyValue in graph[v].Where(keyValue => !mark.Contains(keyValue.Key)))
            {
                mark.Add(keyValue.Key);
                yield return keyValue.Value;

                foreach (var edge in EdgesVisit(graph, keyValue.Key, mark))
                    yield return edge;
            }
        }
    }
}
