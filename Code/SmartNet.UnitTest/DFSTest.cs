using NUnit.Framework;
using SmartNet.Algorithms.Traversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.UnitTest
{
    [TestFixture]
    public class DFSTest
    {

        SGraph<int> intGraph;
        SEdge<int>[] arrayEdge;

        [SetUp]
        public void Init()
        {
            arrayEdge = new SEdge<int>[]{
                new SEdge<int>(1, 2), new SEdge<int>(2, 3), new SEdge<int>(3, 4),
                new SEdge<int>(4, 5), new SEdge<int>(5, 6)
            };

            intGraph = new SGraph<int>();
        }

        [Test]
        public void EdgesDFSForPathGraph()
        {
            intGraph.AddPath(new int[] { 1, 2, 3, 4, 5, 6 });

            var check = new [] { new SEdge<int>(3, 2), new SEdge<int>(2, 1), 
                new SEdge<int>(3, 4), new SEdge<int>(4, 5), new SEdge<int>(5, 6) };


            CheckValues(check, DFS.Edges(intGraph, 3).ToArray());

        }

        [Test]
        public void EdgesDFSForTreeGraph()
        {
            intGraph.AddEdges(arrayEdge);
            intGraph.AddEdge(7, 8);
            intGraph.AddEdge(10, 4);

            var check = new SEdge<int>[arrayEdge.Length + 2];

            for (int i = 0; i < arrayEdge.Length; i++)
            {
                check[i] = arrayEdge[i];
            }
            check[arrayEdge.Length] = new SEdge<int>(4, 10);
            check[arrayEdge.Length + 1] = new SEdge<int>(7, 8);

            var resul = DFS.Edges(intGraph).ToArray();

            CheckValues(check, resul);
        }

        [Test]
        public void EdgesDFSForNonTreeGraph()
        {
            intGraph.AddEdge(10, 3);
            intGraph.AddEdges(arrayEdge);

            intGraph.AddEdge(5, 10);
            intGraph.AddEdge(10, 6);


            var check = new SEdge<int>[arrayEdge.Length + 1];

            check[0] = new SEdge<int>(10, 3);
            check[1] = new SEdge<int>(3, 2);
            check[2] = new SEdge<int>(2, 1);
            check[3] = new SEdge<int>(3, 4);
            check[4] = new SEdge<int>(4, 5);
            check[5] = new SEdge<int>(5, 6);

            var resul = DFS.Edges(intGraph).ToArray();
            CheckValues(check, resul);
        }

        [Test]
        public void DFSTreeFromGraph()
        {
            intGraph.AddEdges(arrayEdge);
            intGraph.AddEdge(new SEdge<int>(6, 1));
            intGraph.AddEdge(new SEdge<int>(5, 2));

            var treeGraph = DFS.Tree(intGraph);

            Assert.AreEqual(intGraph.NumberOfVertices, treeGraph.NumberOfVertices);
            Assert.AreEqual(5, treeGraph.NumberOfEdges);
        }

        [Test]
        public void DFSTreeFromVertex()
        {
            intGraph.AddPath(1, 2, 3, 4, 5, 6, 7);

            intGraph.AddEdge(0, 9);
            intGraph.AddEdge(0, 14);

            var treeGraph = DFS.Tree(intGraph, 9);

            Assert.AreEqual(3, treeGraph.NumberOfVertices);
            Assert.AreEqual(2, treeGraph.NumberOfEdges);
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
