using SmartNet.Algorithms.Traversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var intGraph = new Graph<int>();
            intGraph.AddEdge(10, 3);
            var arrayEdge = new Edge<int>[]{
                new Edge<int>(1, 2), new Edge<int>(2, 3), new Edge<int>(3, 4),
                new Edge<int>(4, 5), new Edge<int>(5, 6)
            };

            intGraph.AddEdges(arrayEdge);
            
            intGraph.AddEdge(5, 10);
            intGraph.AddEdge(10, 6);

            //foreach (var num in intGraph.VerticesIterator)
            //{
            //    Console.Write(num + " ");
            //}

            //Console.WriteLine();

            Edge<int>[] check = new Edge<int>[arrayEdge.Length + 1];

            check[0] = new Edge<int>(10, 3);
            check[1] = new Edge<int>(3, 2);
            check[2] = new Edge<int>(2, 1);
            check[3] = new Edge<int>(3, 4);
            check[4] = new Edge<int>(4, 5);
            check[5] = new Edge<int>(5, 6);

            var resul = DFS.Edges(intGraph).ToArray();

            for (int i = 0; i < resul.Length; i++)
            {
                Console.WriteLine("{0}: {1} == {2}", i, resul[i], check[i]);
            }

        }
    }
}
