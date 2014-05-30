using NUnit.Framework;
using SmartNet.Algorithms.Traversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.UnitTest
{
    [TestFixture]
    public class DFSTest
    {

        Graph<int> intGraph;
        Edge<int>[] arrayEdge;

        [SetUp]
        public void Init()
        {
            arrayEdge = new Edge<int>[]{
                new Edge<int>(1, 2), new Edge<int>(2, 3), new Edge<int>(3, 4),
                new Edge<int>(4, 5), new Edge<int>(5, 6)
            };

            intGraph = new Graph<int>();
        }

        [Test]
        public void EdgesDFSForPathGraph()
        {
            intGraph.AddPath(new int[] { 1, 2, 3, 4, 5, 6 });

            var check = new Edge<int>[] { new Edge<int>(3, 2), new Edge<int>(2, 1), 
                new Edge<int>(3, 4), new Edge<int>(4, 5), new Edge<int>(5, 6) };

            CheckValues(DFS.Edges(intGraph, 3).ToArray(), check);

        }

        [Test]
        public void EdgesDFSForTreeGraph()
        {
            intGraph.AddEdges(arrayEdge);
            intGraph.AddEdge(7, 8);
            intGraph.AddEdge(10, 4);

            Edge<int>[] check = new Edge<int>[arrayEdge.Length + 2];

            for (int i = 0; i < arrayEdge.Length; i++)
			{
			    check[i] = arrayEdge[i];
			}
            check[arrayEdge.Length] = new Edge<int>(4, 10);
            check[arrayEdge.Length + 1] = new Edge<int>(7, 8);

            var resul = DFS.Edges(intGraph).ToArray();

            CheckValues(resul, check);
        }

        [Test]
        public void EdgesDFSForNonTreeGraph()
        {
            intGraph.AddEdge(10, 3);
            intGraph.AddEdges(arrayEdge);

            intGraph.AddEdge(5, 10);
            intGraph.AddEdge(10, 6);


            Edge<int>[] check = new Edge<int>[arrayEdge.Length + 1];

            check[0] = new Edge<int>(10, 3);
            check[1] = new Edge<int>(3, 2);
            check[2] = new Edge<int>(2, 1);
            check[3] = new Edge<int>(3, 4);
            check[4] = new Edge<int>(4, 5);
            check[5] = new Edge<int>(5, 6);

            var resul = DFS.Edges(intGraph).ToArray();
            CheckValues(resul, check);
        }


        [Test]
        public void DFSTreeFromGraph()
        {
            intGraph.AddEdges(arrayEdge);
            intGraph.AddEdge(new Edge<int>(6, 1));
            intGraph.AddEdge(new Edge<int>(5, 2));

            var treeGraph = DFS.Tree(intGraph);

            Assert.AreEqual(treeGraph.NumberOfVertices, intGraph.NumberOfVertices);
            Assert.AreEqual(treeGraph.NumberOfEdges, 5);
        }

        # region Private Methods

        private void CheckValues<T>(T[] array1, T[] array2)
        {
            Assert.AreEqual(array1.Length, array2.Length);

            for (int i = 0; i < array1.Length; i++)
            {
                Assert.AreEqual(array1[i], array2[i]);
            }
        }

        # endregion

    }
}
