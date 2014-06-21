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

            Assert.AreEqual(graph.NumberOfEdges, 0);
            Assert.AreEqual(graph.NumberOfVertices, 0);

        }

        [Test]
        public void K5GraphTest()
        {
            var graph = Classic.CompleteGraph(5);

            Assert.AreEqual(graph.NumberOfVertices, 5);
            Assert.AreEqual(graph.NumberOfEdges, 10);

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

            Assert.AreEqual(graph.NumberOfVertices, 0);
            Assert.AreEqual(graph.NumberOfEdges, 0);
        }

        [Test]
        public void K5DiGraphTest()
        {
            var graph = Classic.CompleteDiGraph(5);

            Assert.AreEqual(graph.NumberOfVertices, 5);
            Assert.AreEqual(graph.NumberOfEdges, 20);


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

            Assert.AreEqual(graph.NumberOfVertices, 0);
            Assert.AreEqual(graph.NumberOfEdges, 0);
        }

        [Test]
        public void K55GraphTest()
        {
            var graph = Classic.CompleteBipartiteGraph(5, 5);

            Assert.AreEqual(graph.NumberOfVertices, 10);
            Assert.AreEqual(graph.NumberOfEdges, 25);

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

        [Test]
        public void K00DiGraphTest()
        {
            var graph = Classic.CompleteBipartiteDiGraph(0, 0);

            Assert.AreEqual(graph.NumberOfVertices, 0);
            Assert.AreEqual(graph.NumberOfEdges, 0);
        }

        [Test]
        public void K55DiGraphTest()
        {
            var graph = Classic.CompleteBipartiteDiGraph(5, 5);

            Assert.AreEqual(graph.NumberOfVertices, 10);
            Assert.AreEqual(graph.NumberOfEdges, 50);

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

            Assert.AreEqual(graph.NumberOfVertices, 6);
            Assert.AreEqual(graph.NumberOfEdges, 5);

            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(0, 2) && graph.HasEdge(2, 0));
            Assert.IsTrue(graph.HasEdge(0, 3) && graph.HasEdge(3, 0));
            Assert.IsTrue(graph.HasEdge(0, 4) && graph.HasEdge(4, 0));
            Assert.IsTrue(graph.HasEdge(0, 5) && graph.HasEdge(5, 0));
        }

        [Test]
        public void Star0DiGraphTest()
        {
            var graph = Classic.StarDiGraph(0);

            Assert.AreEqual(1, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
        }


        [Test]
        public void Star5DiGraphTest()
        {
            var graph = Classic.StarDiGraph(5);

            Assert.AreEqual(graph.NumberOfVertices, 6);
            Assert.AreEqual(graph.NumberOfEdges, 10);

            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(0, 2) && graph.HasEdge(2, 0));
            Assert.IsTrue(graph.HasEdge(0, 3) && graph.HasEdge(3, 0));
            Assert.IsTrue(graph.HasEdge(0, 4) && graph.HasEdge(4, 0));
            Assert.IsTrue(graph.HasEdge(0, 5) && graph.HasEdge(5, 0));
        }

        # endregion

    }
}
