using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SmartNet.Algorithms.Traversal;

namespace SmartNet.UnitTest
{
    [TestFixture]
    public class BFSTest
    {

        private Graph<string, Edge<string>> graph;

        private List<string> stringData;

        [SetUp]
        public void Init()
        {
            graph = new Graph<string, Edge<string>>();
            stringData = new List<string> {"hello", "black", "watchout", "game of thrones", "maroon 5"};
        }

        [Test]
        public void BFSForPathGraph()
        {
            graph.AddPath(stringData);

            var expectedVertices = new string[] {"watchout", "black", "game of thrones", "hello", "maroon 5"};

            CheckValues(expectedVertices, BFS.Vertices(graph, "watchout").ToArray());

        }

        [Test]
        public void BFSForCycleGraph()
        {
            graph.AddCycle(stringData);

            var expectedVertices = new string[] { "maroon 5", "game of thrones", "hello", "watchout", "black" };

            CheckValues(expectedVertices, BFS.Vertices(graph, "maroon 5").ToArray());
        }

        [Test]
        public void EdgeBFSForPathGraph()
        {
            graph.AddPath(stringData[1], stringData[3], stringData[0]);

            var expectedEdges = new Edge<string>[]
            {
                new Edge<string>(stringData[3], stringData[1]),
                new Edge<string>(stringData[3], stringData[0]),
            };

            CheckValues(expectedEdges, BFS.Edges(graph, stringData[3]).ToArray());
        }

        [Test]
        public void EdgeBFSForCycleGraph()
        {
            graph.AddCycle(stringData);

            var expectedEdges = new[]
            {
                new Edge<string>(stringData[1], stringData[0]),
                new Edge<string>(stringData[1], stringData[2]),
                new Edge<string>(stringData[0], stringData[4]),
                new Edge<string>(stringData[2], stringData[3]) 
            };

            CheckValues(expectedEdges, BFS.Edges(graph, "black").ToArray());
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
