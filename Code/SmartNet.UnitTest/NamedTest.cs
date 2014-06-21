﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SmartNet.Models;

namespace SmartNet.UnitTest
{
    [TestFixture]
    public class NamedTest
    {

        [Test]
        public void PetersenGraphTest()
        {
            var graph = Named.PetersenGraph();

            Assert.AreEqual(graph.NumberOfVertices, 10);
            Assert.AreEqual(graph.NumberOfEdges, 15);

            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(1, 2) && graph.HasEdge(2, 1));
            Assert.IsTrue(graph.HasEdge(2, 3) && graph.HasEdge(3, 2));
            Assert.IsTrue(graph.HasEdge(3, 4) && graph.HasEdge(4, 3));
            Assert.IsTrue(graph.HasEdge(4, 0) && graph.HasEdge(0, 4));

            Assert.IsTrue(graph.HasEdge(5, 7) && graph.HasEdge(7, 5));
            Assert.IsTrue(graph.HasEdge(7, 9) && graph.HasEdge(9, 7));
            Assert.IsTrue(graph.HasEdge(9, 6) && graph.HasEdge(6, 9));
            Assert.IsTrue(graph.HasEdge(6, 8) && graph.HasEdge(8, 6));
            Assert.IsTrue(graph.HasEdge(8, 5) && graph.HasEdge(5, 8));

            Assert.IsTrue(graph.HasEdge(0, 5) && graph.HasEdge(5, 0));
            Assert.IsTrue(graph.HasEdge(1, 6) && graph.HasEdge(6, 1));
            Assert.IsTrue(graph.HasEdge(2, 7) && graph.HasEdge(7, 2));
            Assert.IsTrue(graph.HasEdge(3, 8) && graph.HasEdge(8, 3));
            Assert.IsTrue(graph.HasEdge(4, 9) && graph.HasEdge(9, 4));

        }

        [Test]
        public void PetersenDiGraphTest()
        {
            var graph = Named.PetersenDiGraph();

            Assert.AreEqual(graph.NumberOfVertices, 10);
            Assert.AreEqual(graph.NumberOfEdges, 30);

            Assert.IsTrue(graph.HasEdge(0, 1) && graph.HasEdge(1, 0));
            Assert.IsTrue(graph.HasEdge(1, 2) && graph.HasEdge(2, 1));
            Assert.IsTrue(graph.HasEdge(2, 3) && graph.HasEdge(3, 2));
            Assert.IsTrue(graph.HasEdge(3, 4) && graph.HasEdge(4, 3));
            Assert.IsTrue(graph.HasEdge(4, 0) && graph.HasEdge(0, 4));

            Assert.IsTrue(graph.HasEdge(5, 7) && graph.HasEdge(7, 5));
            Assert.IsTrue(graph.HasEdge(7, 9) && graph.HasEdge(9, 7));
            Assert.IsTrue(graph.HasEdge(9, 6) && graph.HasEdge(6, 9));
            Assert.IsTrue(graph.HasEdge(6, 8) && graph.HasEdge(8, 6));
            Assert.IsTrue(graph.HasEdge(8, 5) && graph.HasEdge(5, 8));

            Assert.IsTrue(graph.HasEdge(0, 5) && graph.HasEdge(5, 0));
            Assert.IsTrue(graph.HasEdge(1, 6) && graph.HasEdge(6, 1));
            Assert.IsTrue(graph.HasEdge(2, 7) && graph.HasEdge(7, 2));
            Assert.IsTrue(graph.HasEdge(3, 8) && graph.HasEdge(8, 3));
            Assert.IsTrue(graph.HasEdge(4, 9) && graph.HasEdge(9, 4));
        }

    }
}
