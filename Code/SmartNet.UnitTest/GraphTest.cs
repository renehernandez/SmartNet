using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet;

namespace SmartNet.UnitTest
{

    [TestFixture]
    public class GraphTest
    {

        public class TestClass : IEquatable<TestClass>
        {

            public int Index { get; set; }

            public TestClass(int index)
            {
                Index = index;
            }

            public bool Equals(TestClass other)
            {
                return Index == other.Index;
            }

            public override int GetHashCode()
            {
                return Index;
            }

        }

        Graph<TestClass, Edge<TestClass, Data>, Data> classGraph;

        List<TestClass> testData;

        List<Edge<TestClass, Data>> testEdgeData;

        [SetUp]
        public void Init()
        {
            classGraph = new Graph<TestClass, Edge<TestClass, Data>, Data>();

            testData = new List<TestClass>() 
            { 
                new TestClass(10), new TestClass(-1), new TestClass(20), 
                new TestClass(0), new TestClass(-20) 
            };

            testEdgeData = new List<Edge<TestClass, Data>>()
            {
                new Edge<TestClass, Data>(new TestClass(24), new TestClass(-35)), 
                new Edge<TestClass, Data>(new TestClass(2345), new TestClass(-8035)),
                new Edge<TestClass, Data>(new TestClass(243), new TestClass(23))
            };
        }

        # region Empty graph constructors

        [Test]
        public void ConstructorEmptyClassGraph()
        {
            Assert.AreEqual(classGraph.NumberOfVertices, 0);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, 0);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        # endregion

        # region Graph constructors with vertices data

        [Test]
        public void ConstructorEnumerableVertexDataClassGraph()
        {
            classGraph = new Graph<TestClass, Edge<TestClass, Data>, Data>(testData);

            Assert.AreEqual(classGraph.NumberOfVertices, testData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, testData.Count);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        [Test]
        public void ConstructorArrayVertexDataClassGraph()
        {
            var array = testData.ToArray();
            classGraph = new Graph<TestClass, Edge<TestClass, Data>, Data>(array[0], array[1], array[2], array[3], array[4]);

            Assert.AreEqual(classGraph.NumberOfVertices, array.Length);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, array.Length);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        # endregion

        # region Graph constructors with Edges data

        [Test]
        public void ConstructorEnumerableEdgeDataClassGraph()
        {
            classGraph = new Graph<TestClass, Edge<TestClass, Data>, Data>(testEdgeData);

            Assert.AreEqual(classGraph.NumberOfVertices, 6);
            Assert.AreEqual(classGraph.NumberOfEdges, 3);
            Assert.AreEqual(classGraph.Vertices.Length, 6);
            Assert.AreEqual(classGraph.Edges.Length, 3);
        }

        [Test]
        public void ConstructorArrayEdgeDataClassGraph()
        {
            var array = testEdgeData.ToArray();
            classGraph = new Graph<TestClass, Edge<TestClass, Data>, Data>(array[0], array[1], array[2]);

            Assert.AreEqual(classGraph.NumberOfVertices, 6);
            Assert.AreEqual(classGraph.NumberOfEdges, 3);
            Assert.AreEqual(classGraph.Vertices.Length, 6);
            Assert.AreEqual(classGraph.Edges.Length, 3);
        }

        # endregion

        # region Adding vertex

        [Test]
        public void AddVertexClassGraph()
        {
            Assert.AreEqual(classGraph.NumberOfVertices, 0);
            Assert.AreEqual(classGraph.Vertices.Length, 0);

            var klass = new TestClass(20);
            classGraph.AddVertex(klass);

            Assert.AreEqual(classGraph.NumberOfVertices, 1);
            Assert.AreEqual(classGraph.Vertices.Length, 1);
        }

        # endregion

        # region Adding Edge for existent nodes

        [Test]
        public void AddEdgeClassGraph()
        {
            var klass1 = new TestClass(2);
            var klass2 = new TestClass(10);

            classGraph.AddVertex(klass1);
            classGraph.AddVertex(klass2);
            Assert.AreEqual(classGraph.NumberOfVertices, 2);

            classGraph.AddEdge(klass2, klass1);

            Assert.AreEqual(classGraph.NumberOfVertices, 2);
            Assert.AreEqual(classGraph.NumberOfEdges, 1);
            Assert.AreEqual(classGraph.Edges.Length, 1);
        }

        # endregion

        # region Adding Edge without nodes

        [Test]
        public void AddEdgeWtihoutVerticesClassGraph()
        {
            classGraph.AddEdge(new TestClass(20), new TestClass(15));

            Assert.AreEqual(classGraph.NumberOfVertices, 2);
            Assert.AreEqual(classGraph.NumberOfEdges, 1);
        }

        # endregion

        # region Adding path without vertex in graph

        [Test]
        public void AddPathNoVertexPresentClassGraph()
        {
            classGraph.AddPath(testData);

            Assert.AreEqual(classGraph.NumberOfVertices, testData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, testData.Count - 1);
        }

        # endregion

        # region Adding path with some vertex in graph

        [Test]
        public void AddPathSomeVertexPresentClassGraph()
        {
            classGraph.AddVertices(testData);

            Assert.AreEqual(classGraph.NumberOfVertices, testData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);

            classGraph.AddPath(testData);
            Assert.AreEqual(classGraph.NumberOfVertices, testData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, testData.Count - 1);
        }

        # endregion

        # region Adding cycle without vertex in graph

        [Test]
        public void AddCycleNoVertexPresentClassGraph()
        {
            classGraph.AddCycle(testData);

            Assert.AreEqual(classGraph.NumberOfVertices, testData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, testData.Count);
        }

        # endregion

        # region Adding cycle with some vertex already present in graph

        [Test]
        public void AddCycleWithSomeVertexPresentClassGraph()
        {
            classGraph.AddVertices(testData[1], testData[0], new TestClass(9826593));


            Assert.AreEqual(classGraph.NumberOfVertices, 3);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);

            classGraph.AddCycle(testData);

            Assert.AreEqual(classGraph.NumberOfVertices, testData.Count + 1);
            Assert.AreEqual(classGraph.NumberOfEdges, testData.Count);
        }

        # endregion

        # region Checking for vertex existence

        [Test]
        public void HasVertexClassGraph()
        {
            var klass = new TestClass(40000);
            Assert.AreEqual(classGraph.HasVertex(klass), false);

            classGraph.AddEdge(testData[0], testData[1]);

            Assert.AreEqual(classGraph.HasVertex(testData[0]), true);
            Assert.AreEqual(classGraph.HasVertex(klass), false);
        }

        # endregion

        # region Checking for Edge existence

        [Test]
        public void HasEdgeClassGraph()
        {
            Assert.AreEqual(classGraph.HasEdge(testEdgeData[0]), false);

            classGraph.AddEdge(testEdgeData[0]);

            Assert.AreEqual(classGraph.HasEdge(testEdgeData[1]), false);
            Assert.AreEqual(classGraph.HasEdge(testEdgeData[0]), true);

        }

        # endregion

        # region Neighbors for vertex

        [Test]
        public void NeighborsForClassGraph()
        {
            classGraph.AddEdges(testEdgeData);
            var neighbors = classGraph.Neighbors(testEdgeData[2].Target);

            Assert.AreEqual(neighbors.Length, 1);

            CheckValues(neighbors, new[] { testEdgeData[2].Source });

        }

        # endregion

        # region Subgraph Tests

        # endregion

        # region AdjacencyList Tests

        # endregion

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
