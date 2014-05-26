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

        List<int> intData;
        List<string> stringData;
        List<ClassTest> classData;

        List<Edge<int>> intEdgeData;
        List<Edge<string>> stringEdgeData;
        List<Edge<ClassTest>> classEdgeData;

        [SetUp]
        public void Init()
        {
            intGraph = new Graph<int>();
            stringGraph = new Graph<string>();
            classGraph = new Graph<ClassTest>();

            intData = new List<int>() { 1, 2, 3, 4, 5 };
            stringData = new List<string>() { "newer", "blackbox", "frickels", "average", "resume" };
            classData = new List<ClassTest>() 
            { 
                new ClassTest(10), new ClassTest(-1), new ClassTest(20), 
                new ClassTest(0), new ClassTest(-20) 
            };

            intEdgeData = new List<Edge<int>>() 
            { 
                new Edge<int>(1, 2), new Edge<int>(3, 4), new Edge<int>(2, 3) 
            };
            stringEdgeData = new List<Edge<string>>() 
            {
                new Edge<string>("together", "fork"), new Edge<string>("replaced", "frozen"),
                new Edge<string>("invalid", "fork")
            };

            classEdgeData = new List<Edge<ClassTest>>()
            {
                new Edge<ClassTest>(new ClassTest(24), new ClassTest(-35)), 
                new Edge<ClassTest>(new ClassTest(2345), new ClassTest(-8035)),
                new Edge<ClassTest>(new ClassTest(243), new ClassTest(23))
            };
        }

        # region Empty graph creation

        [Test]
        public void IntegerGraphCreation()
        {
            intGraph = new Graph<int>();

            Assert.AreEqual(intGraph.NumberOfVertices, 0);
            Assert.AreEqual(intGraph.NumberOfEdges, 0);
            Assert.AreEqual(intGraph.Vertices.Length, 0);
            Assert.AreEqual(intGraph.Edges.Length, 0);
        }

        [Test]
        public void StringGraphCreation()
        {
            stringGraph = new Graph<string>();

            Assert.AreEqual(stringGraph.NumberOfVertices, 0);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);
            Assert.AreEqual(stringGraph.Vertices.Length, 0);
            Assert.AreEqual(stringGraph.Edges.Length, 0);
        }

        [Test]
        public void ClassGraphCreation()
        {
            classGraph = new Graph<ClassTest>();

            Assert.AreEqual(classGraph.NumberOfVertices, 0);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, 0);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }


        # endregion

        # region Graph creation with vertices data

        [Test]
        public void IntegerEnumerableDataGraphCreation()
        {
            intGraph = new Graph<int>(intData);

            Assert.AreEqual(intGraph.NumberOfVertices, intData.Count);
            Assert.AreEqual(intGraph.NumberOfEdges, 0);
            Assert.AreEqual(intGraph.Vertices.Length, intData.Count);
            Assert.AreEqual(intGraph.Edges.Length, 0);
        }

        [Test]
        public void IntegerArrayDataGraphCreatino()
        {
            var arrayData = intData.ToArray();
            intGraph = new Graph<int>(arrayData);

            Assert.AreEqual(intGraph.NumberOfVertices, arrayData.Length);
            Assert.AreEqual(intGraph.NumberOfEdges, 0);
            Assert.AreEqual(intGraph.Vertices.Length, arrayData.Length);
            Assert.AreEqual(intGraph.Edges.Length, 0);
        }

        [Test]
        public void StringEnumerableDataGraphCreation()
        {
            stringGraph = new Graph<string>(stringData);

            Assert.AreEqual(stringGraph.NumberOfVertices, stringData.Count);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);
            Assert.AreEqual(stringGraph.Vertices.Length, stringData.Count);
            Assert.AreEqual(stringGraph.Edges.Length, 0);
        }

        [Test]
        public void StringArrayDataGraphCreation()
        {
            var arrayData = stringData.ToArray();
            stringGraph = new Graph<string>(stringData);

            Assert.AreEqual(stringGraph.NumberOfVertices, arrayData.Length);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);
            Assert.AreEqual(stringGraph.Vertices.Length, arrayData.Length);
            Assert.AreEqual(stringGraph.Edges.Length, 0);
        }

        [Test]
        public void ClassEnumerableDataGraphCreation()
        {
            classGraph = new Graph<ClassTest>(classData);

            Assert.AreEqual(classGraph.NumberOfVertices, classData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, classData.Count);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        [Test]
        public void ClassArrayDataGraphCreation()
        {
            var arrayData = stringData.ToArray();
            classGraph = new Graph<ClassTest>(classData);

            Assert.AreEqual(classGraph.NumberOfVertices, arrayData.Length);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, arrayData.Length);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        # endregion

        # region Graph creation with edges data

        [Test]
        public void IntegerEnumerableEdgeDataGraphCreation()
        {
            intGraph = new Graph<int>(intEdgeData);

            Assert.AreEqual(intGraph.NumberOfVertices, 4);
            Assert.AreEqual(intGraph.NumberOfEdges, 3);
            Assert.AreEqual(intGraph.Vertices.Length, 4);
            Assert.AreEqual(intGraph.Edges.Length, 3);
        }

        [Test]
        public void StringEnumerableEdgeDataGraphCreation()
        {
            stringGraph = new Graph<string>(stringEdgeData);

            Assert.AreEqual(stringGraph.NumberOfVertices, 5);
            Assert.AreEqual(stringGraph.NumberOfEdges, 3);
            Assert.AreEqual(stringGraph.Vertices.Length, 5);
            Assert.AreEqual(stringGraph.Edges.Length, 3);
        }

        [Test]
        public void ClassEnumerableEdgeDataGraphCreation()
        {
            classGraph = new Graph<ClassTest>(classEdgeData);

            Assert.AreEqual(classGraph.NumberOfVertices, 6);
            Assert.AreEqual(classGraph.NumberOfEdges, 3);
            Assert.AreEqual(classGraph.Vertices.Length, 6);
            Assert.AreEqual(classGraph.Edges.Length, 3);
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

        # region Adding edge without nodes

        [Test]
        public void AddEdgeWithoutVerticesIntegerGraph()
        {
            Assert.AreEqual(intGraph.NumberOfEdges, 0);
            Assert.AreEqual(intGraph.NumberOfVertices, 0);

            intGraph.AddEdge(1, 2);

            Assert.AreEqual(intGraph.NumberOfVertices, 2);
            Assert.AreEqual(intGraph.NumberOfEdges, 1);

        }

        [Test]
        public void AddEdgeWithoutVerticesStringGraph()
        {
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);
            Assert.AreEqual(intGraph.NumberOfVertices, 0);

            stringGraph.AddEdge("me", "you");

            Assert.AreEqual(stringGraph.NumberOfEdges, 1);
            Assert.AreEqual(stringGraph.NumberOfVertices, 2);
        }

        [Test]
        public void AddEdgeWtihoutVerticesClassGraph()
        {
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.NumberOfVertices, 0);

            classGraph.AddEdge(new ClassTest(20), new ClassTest(15));

            Assert.AreEqual(classGraph.NumberOfVertices, 2);
            Assert.AreEqual(classGraph.NumberOfEdges, 1);
        }

        # endregion

    }
}
