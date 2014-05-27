using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Algorithms.Traversal
{
    public static class DFS
    {

        public static IEnumerable<Edge<T>> Edges<T>(Graph<T> graph) where T: IEquatable<T>
        {
            return Edges(graph, graph.VerticesIterator.First());
        }

        public static IEnumerable<Edge<T>> Edges<T>(Graph<T> graph, T vertex) where T: IEquatable<T>
        {
            var mark = new Dictionary<T, bool>(graph.NumberOfVertices);

            foreach (var v in graph.VerticesIterator)
                mark[v] = false;

            mark[vertex] = true;

            foreach (var edge in EdgesVisit(graph, vertex, mark))
                yield return edge;

            foreach(var w in graph.VerticesIterator)
            {
                if (mark[w])
                    continue;

                mark[w] = true;
                foreach (var edge in EdgesVisit(graph, w, mark))
                    yield return edge;
            }
            
        }

        private static IEnumerable<Edge<T>> EdgesVisit<T>(Graph<T> graph, T v, Dictionary<T, bool> mark) 
            where T: IEquatable<T>
        {
            foreach (var keyValue in graph[v])
            {
                if (mark[keyValue.Key])
                    continue;

                mark[keyValue.Key] = true;
                yield return keyValue.Value;

                foreach (var edge in EdgesVisit(graph, keyValue.Key, mark))
                    yield return edge;
            }
        }

    }
}
