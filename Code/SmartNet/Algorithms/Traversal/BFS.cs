using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Algorithms.Traversal
{
    public static class BFS
    {

        public static IEnumerable<T> Vertices<T>(Graph<T> graph, T vertex) where T : IEquatable<T>
        {
            var mark = new HashSet<T>() {vertex};
            var queue = new Queue<T>();

            T current;
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

    }
}
