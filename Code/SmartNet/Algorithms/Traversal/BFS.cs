using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Algorithms.Traversal
{
    public static class BFS
    {

        public static IEnumerable<TVertex> Vertices<TVertex, TEdge>(BaseGraph<TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            var mark = new HashSet<TVertex>() {vertex};
            var queue = new Queue<TVertex>();

            TVertex current;
            queue.Enqueue(vertex);

            while (queue.Count != 0)
            {
                current = queue.Dequeue();
                yield return current;

                foreach (var neighbor in graph.NeighborsIterator(current).Where(neighbor => !mark.Contains(neighbor)))
                {
                    mark.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        public static IEnumerable<TEdge> Edges<TVertex, TEdge>(BaseGraph<TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            var mark = new HashSet<TVertex>() {vertex};
            var queue = new Queue<TVertex>();
            queue.Enqueue(vertex);

            TVertex current;
            while (queue.Count != 0)
            {
                current = queue.Dequeue();

                foreach (var adj in graph[current].Where(adj => !mark.Contains(adj.Key)))
                {
                    yield return adj.Value;
                    mark.Add(adj.Key);
                    queue.Enqueue(adj.Key);
                }
            }
        }

        public static BaseGraph<TVertex, TEdge> Tree<TVertex, TEdge>(BaseGraph<TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex>
        {
            var treeGraph = graph.Subgraph();

            treeGraph.AddEdges(Edges(graph, vertex));

            return treeGraph;
        }  

    }
}
