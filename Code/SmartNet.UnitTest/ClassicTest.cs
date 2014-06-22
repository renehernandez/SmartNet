using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SmartNet.Models;

namespace SmartNet.UnitTest
{
    [TestFixture]
    public class ClassicTest
    {


        [SetUp]
        public void Init()
        {
        }

        # region Complete Graph Tests

        [Test]
        public void K0GraphTest()
        {
            var graph = Classic.CompleteGraph(0);

            Assert.AreEqual(0, graph.NumberOfEdges);
            Assert.AreEqual(0, graph.NumberOfVertices);

        }

        [Test]
        public void K5GraphTest()
        {
            var graph = Classic.CompleteGraph(5);

            Assert.AreEqual(5, graph.NumberOfVertices);
            Assert.AreEqual(10, graph.NumberOfEdges);

            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(0, 2) && graph.HasEdge(2, 0));
            Assert.IsTrue(graph.HasEdge(0, 3) && graph.HasEdge(3, 0));
            Assert.IsTrue(graph.HasEdge(0, 4) && graph.HasEdge(4, 0));

            Assert.IsTrue(graph.HasEdge(1, 2) && graph.HasEdge(2, 1));
            Assert.IsTrue(graph.HasEdge(1, 3) && graph.HasEdge(3, 1));
            Assert.IsTrue(graph.HasEdge(1, 4) && graph.HasEdge(4, 1));

            Assert.IsTrue(graph.HasEdge(2, 3) && graph.HasEdge(3, 2));
            Assert.IsTrue(graph.HasEdge(2, 4) && graph.HasEdge(4, 2));

            Assert.IsTrue(graph.HasEdge(3, 4) && graph.HasEdge(4, 3));

        }

        [Test]
        public void K0DiGraphTest()
        {
            var graph = Classic.CompleteDiGraph(0);

            Assert.AreEqual(0, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
        }

        [Test]
        public void K5DiGraphTest()
        {
            var graph = Classic.CompleteDiGraph(5);

            Assert.AreEqual(5, graph.NumberOfVertices);
            Assert.AreEqual(20, graph.NumberOfEdges);


            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(0, 2) && graph.HasEdge(2, 0));
            Assert.IsTrue(graph.HasEdge(0, 3) && graph.HasEdge(3, 0));
            Assert.IsTrue(graph.HasEdge(0, 4) && graph.HasEdge(4, 0));

            Assert.IsTrue(graph.HasEdge(1, 2) && graph.HasEdge(2, 1));
            Assert.IsTrue(graph.HasEdge(1, 3) && graph.HasEdge(3, 1));
            Assert.IsTrue(graph.HasEdge(1, 4) && graph.HasEdge(4, 1));

            Assert.IsTrue(graph.HasEdge(2, 3) && graph.HasEdge(3, 2));
            Assert.IsTrue(graph.HasEdge(2, 4) && graph.HasEdge(4, 2));

            Assert.IsTrue(graph.HasEdge(3, 4) && graph.HasEdge(4, 3));
        }

        # endregion

        # region Complete Bipartite Tests

        [Test]
        public void K00GraphTest()
        {
            var graph = Classic.CompleteBipartiteGraph(0, 0);

            Assert.AreEqual(0, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
        }

        [Test]
        public void K55GraphTest()
        {
            var graph = Classic.CompleteBipartiteGraph(5, 5);

            Assert.AreEqual(10, graph.NumberOfVertices);
            Assert.AreEqual(25, graph.NumberOfEdges);

            Assert.IsTrue(graph.HasEdge(0, 5) && graph.HasEdge(5, 0));
            Assert.IsTrue(graph.HasEdge(0, 6) && graph.HasEdge(6, 0));
            Assert.IsTrue(graph.HasEdge(0, 7) && graph.HasEdge(7, 0));
            Assert.IsTrue(graph.HasEdge(0, 8) && graph.HasEdge(8, 0));
            Assert.IsTrue(graph.HasEdge(0, 9) && graph.HasEdge(9, 0));

            Assert.IsTrue(graph.HasEdge(1, 5) && graph.HasEdge(5, 1));
            Assert.IsTrue(graph.HasEdge(1, 6) && graph.HasEdge(6, 1));
            Assert.IsTrue(graph.HasEdge(1, 7) && graph.HasEdge(7, 1));
            Assert.IsTrue(graph.HasEdge(1, 8) && graph.HasEdge(8, 1));
            Assert.IsTrue(graph.HasEdge(1, 9) && graph.HasEdge(9, 1));

            Assert.IsTrue(graph.HasEdge(2, 5) && graph.HasEdge(5, 2));
            Assert.IsTrue(graph.HasEdge(2, 6) && graph.HasEdge(6, 2));
            Assert.IsTrue(graph.HasEdge(2, 7) && graph.HasEdge(7, 2));
            Assert.IsTrue(graph.HasEdge(2, 8) && graph.HasEdge(8, 2));
            Assert.IsTrue(graph.HasEdge(2, 9) && graph.HasEdge(9, 2));

            Assert.IsTrue(graph.HasEdge(3, 5) && graph.HasEdge(5, 3));
            Assert.IsTrue(graph.HasEdge(3, 6) && graph.HasEdge(6, 3));
            Assert.IsTrue(graph.HasEdge(3, 7) && graph.HasEdge(7, 3));
            Assert.IsTrue(graph.HasEdge(3, 8) && graph.HasEdge(8, 3));
            Assert.IsTrue(graph.HasEdge(3, 9) && graph.HasEdge(9, 3));

            Assert.IsTrue(graph.HasEdge(4, 5) && graph.HasEdge(5, 4));
            Assert.IsTrue(graph.HasEdge(4, 6) && graph.HasEdge(6, 4));
            Assert.IsTrue(graph.HasEdge(4, 7) && graph.HasEdge(7, 4));
            Assert.IsTrue(graph.HasEdge(4, 8) && graph.HasEdge(8, 4));
            Assert.IsTrue(graph.HasEdge(4, 9) && graph.HasEdge(9, 4));
        }

        # endregion

        # region Star Graphs

        [Test]
        public void Star0GraphTest()
        {
            var graph = Classic.StarGraph(0);

            Assert.AreEqual(1, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
        }


        [Test]
        public void Star5GraphTest()
        {
            var graph = Classic.StarGraph(5);

            Assert.AreEqual(6, graph.NumberOfVertices);
            Assert.AreEqual(5, graph.NumberOfEdges);

            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(0, 2) && graph.HasEdge(2, 0));
            Assert.IsTrue(graph.HasEdge(0, 3) && graph.HasEdge(3, 0));
            Assert.IsTrue(graph.HasEdge(0, 4) && graph.HasEdge(4, 0));
            Assert.IsTrue(graph.HasEdge(0, 5) && graph.HasEdge(5, 0));
        }

        # endregion

        # region Balanced Tree

        [Test]
        public void Balanced10TreeTest()
        {
            var graph = Classic.BalancedTree(1, 0);

            Assert.AreEqual(1, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
        }

        [Test]
        public void Balanced50TreeTest()
        {
            var graph = Classic.BalancedTree(5, 0);

            Assert.AreEqual(1, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
        }

        [Test]
        public void Balanced32TreeTest()
        {
            var graph = Classic.BalancedTree(3, 2);

            Assert.AreEqual(13, graph.NumberOfVertices);
            Assert.AreEqual(12, graph.NumberOfEdges);

            Assert.AreEqual(3, graph.Degree(0));
            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(0, 2) && graph.HasEdge(2, 0));
            Assert.IsTrue(graph.HasEdge(0, 3) && graph.HasEdge(3, 0));

            Assert.IsTrue(graph.HasEdge(1, 4) && graph.HasEdge(4, 1));
            Assert.IsTrue(graph.HasEdge(1, 5) && graph.HasEdge(5, 1));
            Assert.IsTrue(graph.HasEdge(1, 6) && graph.HasEdge(6, 1));

            Assert.IsTrue(graph.HasEdge(2, 7) && graph.HasEdge(7, 2));
            Assert.IsTrue(graph.HasEdge(2, 8) && graph.HasEdge(8, 2));
            Assert.IsTrue(graph.HasEdge(2, 9) && graph.HasEdge(9, 2));

            Assert.IsTrue(graph.HasEdge(3, 10) && graph.HasEdge(10, 3));
            Assert.IsTrue(graph.HasEdge(3, 11) && graph.HasEdge(11, 3));
            Assert.IsTrue(graph.HasEdge(3, 12) && graph.HasEdge(12, 3));

        }

        # endregion

    }
}
