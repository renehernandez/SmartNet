using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet;

namespace GraphNet.TestBase
{

    [TestFixture]
    public class GraphTest
    {

        public class ClassTest : IEquatable<ClassTest>
        {

            public int Index { get; set; }


            public ClassTest(int index)
            {
                Index = index;
            }

            public bool Equals(ClassTest other)
            {
                return Index == other.Index;
            }

            public override int GetHashCode()
            {
                return Index;
            }

        }

        Graph<int> intGraph;
        
        Graph<string> stringGraph;

        Graph<ClassTest> classGraph;

        [SetUp]
        public void Init()
        {
            intGraph = new Graph<int>();
            stringGraph = new Graph<string>();
            classGraph = new Graph<ClassTest>();
        }

        # region Graph creation

        [Test]
        public void IntegerGraphCreation()
        {
            intGraph = new Graph<int>();
        }

        [Test]
        public void StringGraphCreation()
        {
            stringGraph = new Graph<string>();
        }

        [Test]
        public void ClassGraphCreation()
        {
            classGraph = new Graph<ClassTest>();
        }


        # endregion

        # region Initial values in graph creation

        [Test]
        public void InitialValuesIntegerGgraph()
        {
            Assert.AreEqual(intGraph.NumberOfVertices, 0);
            Assert.AreEqual(intGraph.NumberOfEdges, 0);

            Assert.AreEqual(intGraph.Vertices.Length, 0);
            Assert.AreEqual(intGraph.Edges.Length, 0);
        }

        [Test]
        public void InitialValuesStringGgraph()
        {
            Assert.AreEqual(stringGraph.NumberOfVertices, 0);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);

            Assert.AreEqual(stringGraph.Vertices.Length, 0);
            Assert.AreEqual(stringGraph.Edges.Length, 0);
        }

        [Test]
        public void InitialValuesClassGgraph()
        {
            Assert.AreEqual(classGraph.NumberOfVertices, 0);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);

            Assert.AreEqual(classGraph.Vertices.Length, 0);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        # endregion

        # region Adding vertex

        [Test]
        public void AddVertexIntegerGraph()
        {
            intGraph.AddVertex(1);

            Assert.AreEqual(intGraph.NumberOfVertices, 1);
            Assert.AreEqual(intGraph.Vertices.Length, 1);
        }

        [Test]
        public void AddVertexStringGraph()
        {
            stringGraph.AddVertex("hello");

            Assert.AreEqual(stringGraph.NumberOfVertices, 1);
            Assert.AreEqual(stringGraph.Vertices.Length, 1);
        }

        [Test]
        public void AddVertexClassGraph()
        {
            var klass = new ClassTest(20);
            classGraph.AddVertex(klass);

            Assert.AreEqual(classGraph.NumberOfVertices, 1);
            Assert.AreEqual(classGraph.Vertices.Length, 1);
        }

        # endregion

        # region Adding edge for existent nodes

        [Test]
        public void AddEdgeIntegerGraph()
        {
            intGraph.AddVertex(1);
            intGraph.AddVertex(2);
            Assert.AreEqual(intGraph.NumberOfVertices, 2);
            
            intGraph.AddEdge(1, 2);

            Assert.AreEqual(intGraph.NumberOfVertices, 2);
            Assert.AreEqual(intGraph.NumberOfEdges, 1);
            Assert.AreEqual(intGraph.Edges.Length, 1);
        }

        [Test]
        public void AddEdgeStringGraph()
        {
            stringGraph.AddVertex("me");
            stringGraph.AddVertex("you");
            Assert.AreEqual(stringGraph.NumberOfVertices, 2);

            stringGraph.AddEdge("me", "you");

            Assert.AreEqual(stringGraph.NumberOfVertices, 2);
            Assert.AreEqual(stringGraph.NumberOfEdges, 1);
            Assert.AreEqual(stringGraph.Edges.Length, 1);
        }

        [Test]
        public void AddEdgeClassGraph()
        {
            var klass1 = new ClassTest(2);
            var klass2 = new ClassTest(10);

            classGraph.AddVertex(klass1);
            classGraph.AddVertex(klass2);
            Assert.AreEqual(classGraph.NumberOfVertices, 2);

            classGraph.AddEdge(klass2, klass1);

            Assert.AreEqual(classGraph.NumberOfVertices, 2);
            Assert.AreEqual(classGraph.NumberOfEdges, 1);
            Assert.AreEqual(classGraph.Edges.Length, 1);
        }

        # endregion

    }
}
