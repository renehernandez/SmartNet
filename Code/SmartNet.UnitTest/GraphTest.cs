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

        Graph<TestClass, Edge<TestClass, EdgeData>, GraphData, EdgeData> graph;

        List<TestClass> data;

        List<Edge<TestClass, EdgeData>> edgeData;

        [SetUp]
        public void Init()
        {
            graph = new Graph<TestClass, Edge<TestClass, EdgeData>, GraphData, EdgeData>();

            data = new List<TestClass>() 
            { 
                new TestClass(10), new TestClass(-1), new TestClass(20), 
                new TestClass(0), new TestClass(-20) 
            };

            edgeData = new List<Edge<TestClass, EdgeData>>()
            {
                new Edge<TestClass, EdgeData>(new TestClass(24), new TestClass(-35)), 
                new Edge<TestClass, EdgeData>(new TestClass(2345), new TestClass(-8035)),
                new Edge<TestClass, EdgeData>(new TestClass(243), new TestClass(23))
            };
        }

        # region Empty graph constructors

        [Test]
        public void ConstructorEmptyClassGraph()
        {
            Assert.AreEqual(0, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
            Assert.AreEqual(0, graph.Vertices.Length);
            Assert.AreEqual(0, graph.Edges.Length);
        }

        # endregion

        # region Graph constructors with vertices data

        [Test]
        public void ConstructorEnumerableVertexDataClassGraph()
        {
            graph = new Graph<TestClass, Edge<TestClass, EdgeData>, GraphData, EdgeData>(data);

            Assert.AreEqual(data.Count, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
            Assert.AreEqual(data.Count, graph.Vertices.Length);
            Assert.AreEqual(0, graph.Edges.Length);
        }

        [Test]
        public void ConstructorArrayVertexDataClassGraph()
        {
            var array = data.ToArray();
            graph = new Graph<TestClass, Edge<TestClass, EdgeData>, GraphData, EdgeData>(array[0], array[1], array[2], array[3], array[4]);

            Assert.AreEqual(array.Length, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);
            Assert.AreEqual(array.Length, graph.Vertices.Length);
            Assert.AreEqual(0, graph.Edges.Length);
        }

        # endregion

        # region Graph constructors with Edges data

        [Test]
        public void ConstructorEnumerableEdgeDataClassGraph()
        {
            graph = new Graph<TestClass, Edge<TestClass, EdgeData>, GraphData, EdgeData>(edgeData);

            Assert.AreEqual(6, graph.NumberOfVertices);
            Assert.AreEqual(3, graph.NumberOfEdges);
            Assert.AreEqual(6, graph.Vertices.Length);
            Assert.AreEqual(3, graph.Edges.Length);
        }

        [Test]
        public void ConstructorArrayEdgeDataClassGraph()
        {
            var array = edgeData.ToArray();
            graph = new Graph<TestClass, Edge<TestClass, EdgeData>, GraphData, EdgeData>(array[0], array[1], array[2]);

            Assert.AreEqual(6, graph.NumberOfVertices);
            Assert.AreEqual(3, graph.NumberOfEdges);
            Assert.AreEqual(6, graph.Vertices.Length);
            Assert.AreEqual(3, graph.Edges.Length);
        }

        # endregion

        # region Adding vertex

        [Test]
        public void AddVertexClassGraph()
        {
            Assert.AreEqual(0, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.Vertices.Length);

            var klass = new TestClass(20);
            graph.AddVertex(klass);

            Assert.AreEqual(1, graph.NumberOfVertices);
            Assert.AreEqual(1, graph.Vertices.Length);
        }

        # endregion

        # region Adding Edge for existent nodes

        [Test]
        public void AddEdgeClassGraph()
        {
            var klass1 = new TestClass(2);
            var klass2 = new TestClass(10);

            graph.AddVertex(klass1);
            graph.AddVertex(klass2);
            Assert.AreEqual(2, graph.NumberOfVertices);

            graph.AddEdge(klass2, klass1);

            Assert.AreEqual(2, graph.NumberOfVertices);
            Assert.AreEqual(1, graph.NumberOfEdges);
            Assert.AreEqual(1, graph.Edges.Length);
        }

        # endregion

        # region Adding Edge without nodes

        [Test]
        public void AddEdgeWtihoutVerticesClassGraph()
        {
            graph.AddEdge(new TestClass(20), new TestClass(15));

            Assert.AreEqual(2, graph.NumberOfVertices);
            Assert.AreEqual(1, graph.NumberOfEdges);
        }

        # endregion

        # region Adding path without vertex in graph

        [Test]
        public void AddPathNoVertexPresentClassGraph()
        {
            graph.AddPath(data);

            Assert.AreEqual(data.Count, graph.NumberOfVertices);
            Assert.AreEqual(data.Count - 1, graph.NumberOfEdges);
        }

        # endregion

        # region Adding path with some vertex in graph

        [Test]
        public void AddPathSomeVertexPresentClassGraph()
        {
            graph.AddVertices(data);

            Assert.AreEqual(data.Count, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);

            graph.AddPath(data);
            Assert.AreEqual(data.Count, graph.NumberOfVertices);
            Assert.AreEqual(data.Count - 1, graph.NumberOfEdges);
        }

        # endregion

        # region Adding cycle without vertex in graph

        [Test]
        public void AddCycleNoVertexPresentClassGraph()
        {
            graph.AddCycle(data);

            Assert.AreEqual(data.Count, graph.NumberOfVertices);
            Assert.AreEqual(data.Count, graph.NumberOfEdges);
        }

        # endregion

        # region Adding cycle with some vertex already present in graph

        [Test]
        public void AddCycleWithSomeVertexPresentClassGraph()
        {
            graph.AddVertices(data[1], data[0], new TestClass(9826593));


            Assert.AreEqual(3, graph.NumberOfVertices);
            Assert.AreEqual(0, graph.NumberOfEdges);

            graph.AddCycle(data);

            Assert.AreEqual(data.Count + 1, graph.NumberOfVertices);
            Assert.AreEqual(data.Count, graph.NumberOfEdges);
        }

        # endregion

        # region Checking for vertex existence

        [Test]
        public void HasVertexClassGraph()
        {
            var klass = new TestClass(40000);
            Assert.AreEqual(false, graph.HasVertex(klass));

            graph.AddEdge(data[0], data[1]);

            Assert.AreEqual(true, graph.HasVertex(data[0]));
            Assert.AreEqual(false, graph.HasVertex(klass));
        }

        # endregion

        # region Checking for Edge existence

        [Test]
        public void HasEdgeClassGraph()
        {
            Assert.AreEqual(false, graph.HasEdge(edgeData[0]));

            graph.AddEdge(edgeData[0]);

            Assert.AreEqual(false, graph.HasEdge(edgeData[1]));
            Assert.AreEqual(true, graph.HasEdge(edgeData[0]));

        }

        # endregion

        # region Neighbors for vertex

        [Test]
        public void NeighborsForClassGraph()
        {
            graph.AddEdges(edgeData);
            var neighbors = graph.Neighbors(edgeData[2].Target);

            Assert.AreEqual(1, neighbors.Length);

            CheckValues(new[] { edgeData[2].Source }, neighbors);

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
