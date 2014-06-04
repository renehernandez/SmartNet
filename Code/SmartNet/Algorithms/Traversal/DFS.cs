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
            return Vertices(graph, graph.VerticesIterator.First());
        }

        public static IEnumerable<TVertex> Vertices<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex) 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            var mark = new Dictionary<TVertex, bool>();

            foreach (var w in graph.VerticesIterator)
                mark[w] = false;

            mark[vertex] = true;

            foreach (var w in VerticesVisit(graph, vertex, mark))
                yield return w;

            foreach (var w in graph.VerticesIterator)
            {
                if (mark[w])
                    continue;

                mark[w] = true;

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }

        }

        private static IEnumerable<TVertex> VerticesVisit<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex, Dictionary<TVertex, bool> mark) 
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            foreach (var w in graph.NeighborsIterator(vertex))
            {
                if (mark[w])
                    continue;

                mark[w] = true;
                yield return w;

                foreach (var v in VerticesVisit(graph, w, mark))
                    yield return v;
            }
        }

        public static IEnumerable<TEdge> Edges<TVertex, TEdge>(Graph<TVertex, TEdge> graph)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            return Edges(graph, graph.VerticesIterator.First());
        }

        public static IEnumerable<TEdge> Edges<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            var mark = new Dictionary<TVertex, bool>(graph.NumberOfVertices);

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

        private static IEnumerable<TEdge> EdgesVisit<TVertex, TEdge>(Graph<TVertex, TEdge> graph, TVertex v, Dictionary<TVertex, bool> mark)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
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
