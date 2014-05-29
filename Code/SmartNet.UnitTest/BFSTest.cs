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

        private Graph<string> graph;

        private List<string> stringData;

        [SetUp]
        public void Init()
        {
            graph = new Graph<string>();
            stringData = new List<string> {"hello", "black", "watchout", "game of thrones", "maroon 5"};
        }

        [Test]
        public void FromEndOfPathGraph()
        {
            graph.AddPath(stringData);

            var expectedVertices = new string[] {"watchout", "black", "game of thrones", "hello", "maroon 5"};

            CheckValues(expectedVertices, BFS.Vertices(graph, "watchout").ToArray());

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
