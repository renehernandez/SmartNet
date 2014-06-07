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

        public static IEnumerable<TVertex> Vertices<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex> 
            where TGraph : IGraph<TGraph, TVertex, TEdge>
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

        public static IEnumerable<TEdge> Edges<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex> 
            where TGraph : IGraph<TGraph, TVertex, TEdge>
        {
            var mark = new HashSet<TVertex>() {vertex};
            var queue = new Queue<TVertex>();
            queue.Enqueue(vertex);

            TVertex current;
            while (queue.Count != 0)
            {
                current = queue.Dequeue();

                foreach (var neighborEdge in graph.NeighborsEdgesIterator(current).Where(edge => !mark.Contains(edge.Target)))
                {
                    yield return neighborEdge;
                    mark.Add(neighborEdge.Target);
                    queue.Enqueue(neighborEdge.Target);
                }
            }
        }

        public static TGraph Tree<TGraph, TVertex, TEdge>(IGraph<TGraph, TVertex, TEdge> graph, TVertex vertex)
            where TVertex : IEquatable<TVertex>
            where TEdge : IEdge<TVertex> 
            where TGraph : IGraph<TGraph, TVertex, TEdge>
        {
            var treeGraph = graph.Subgraph();

            treeGraph.AddEdges(Edges(graph, vertex));

            return treeGraph;
        }  

    }
}
