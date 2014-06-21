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

            Assert.IsTrue(graph.HasEdge(0, 1));
            Assert.IsTrue(graph.HasEdge(0, 2));
            Assert.IsTrue(graph.HasEdge(0, 3));
            Assert.IsTrue(graph.HasEdge(0, 4));

            Assert.IsTrue(graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(1, 2));
            Assert.IsTrue(graph.HasEdge(1, 3));
            Assert.IsTrue(graph.HasEdge(1, 4));

            Assert.IsTrue(graph.HasEdge(2, 0));
            Assert.IsTrue(graph.HasEdge(2, 1));
            Assert.IsTrue(graph.HasEdge(2, 3));
            Assert.IsTrue(graph.HasEdge(2, 4));

            Assert.IsTrue(graph.HasEdge(3, 0));
            Assert.IsTrue(graph.HasEdge(3, 1));
            Assert.IsTrue(graph.HasEdge(3, 2));
            Assert.IsTrue(graph.HasEdge(3, 4));

            Assert.IsTrue(graph.HasEdge(4, 0));
            Assert.IsTrue(graph.HasEdge(4, 1));
            Assert.IsTrue(graph.HasEdge(4, 2));
            Assert.IsTrue(graph.HasEdge(4, 3));

        }

    }
}
