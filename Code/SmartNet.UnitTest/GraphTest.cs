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

        Graph<string> stringGraph;
        Graph<ClassTest> classGraph;

        List<string> stringData;
        List<ClassTest> classData;

        List<Edge<string>> stringEdgeData;
        List<Edge<ClassTest>> classEdgeData;

        [SetUp]
        public void Init()
        {
            stringGraph = new Graph<string>();
            classGraph = new Graph<ClassTest>();

            stringData = new List<string>() { "newer", "blackbox", "frickels", "average", "resume" };

            classData = new List<ClassTest>() 
            { 
                new ClassTest(10), new ClassTest(-1), new ClassTest(20), 
                new ClassTest(0), new ClassTest(-20) 
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

        # region Empty graph constructors

        [Test]
        public void ConstructorEmptyStringGraph()
        {
            Assert.AreEqual(stringGraph.NumberOfVertices, 0);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);
            Assert.AreEqual(stringGraph.Vertices.Length, 0);
            Assert.AreEqual(stringGraph.Edges.Length, 0);
        }

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
        public void ConstructorEnumerableVertexDataStringGraph()
        {
            stringGraph = new Graph<string>(stringData);

            Assert.AreEqual(stringGraph.NumberOfVertices, stringData.Count);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);
            Assert.AreEqual(stringGraph.Vertices.Length, stringData.Count);
            Assert.AreEqual(stringGraph.Edges.Length, 0);
        }

        [Test]
        public void ConstructorEnumerableVertexDataClassGraph()
        {
            classGraph = new Graph<ClassTest>(classData);

            Assert.AreEqual(classGraph.NumberOfVertices, classData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, classData.Count);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        [Test]
        public void ConstructorArrayVertexDataStringGraph()
        {
            var array = stringData.ToArray();
            stringGraph = new Graph<string>(array[0], array[1], array[2], array[3], array[4]);

            Assert.AreEqual(stringGraph.NumberOfVertices, array.Length);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);
            Assert.AreEqual(stringGraph.Vertices.Length, array.Length);
            Assert.AreEqual(stringGraph.Edges.Length, 0);
        }

        [Test]
        public void ConstructorArrayVertexDataClassGraph()
        {
            var array = classData.ToArray();
            classGraph = new Graph<ClassTest>(array[0], array[1], array[2], array[3], array[4]);

            Assert.AreEqual(classGraph.NumberOfVertices, array.Length);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);
            Assert.AreEqual(classGraph.Vertices.Length, array.Length);
            Assert.AreEqual(classGraph.Edges.Length, 0);
        }

        # endregion

        # region Graph constructors with edges data

        [Test]
        public void ConstructorEnumerableEdgeDataStringGraph()
        {
            stringGraph = new Graph<string>(stringEdgeData);

            Assert.AreEqual(stringGraph.NumberOfVertices, 5);
            Assert.AreEqual(stringGraph.NumberOfEdges, 3);
            Assert.AreEqual(stringGraph.Vertices.Length, 5);
            Assert.AreEqual(stringGraph.Edges.Length, 3);
        }

        [Test]
        public void ConstructorEnumerableEdgeDataClassGraph()
        {
            classGraph = new Graph<ClassTest>(classEdgeData);

            Assert.AreEqual(classGraph.NumberOfVertices, 6);
            Assert.AreEqual(classGraph.NumberOfEdges, 3);
            Assert.AreEqual(classGraph.Vertices.Length, 6);
            Assert.AreEqual(classGraph.Edges.Length, 3);
        }

        [Test]
        public void ConstructorArrayEdgeDataStringGraph()
        {
            var array = stringEdgeData.ToArray();
            stringGraph = new Graph<string>(array[0], array[1], array[2]);

            Assert.AreEqual(stringGraph.NumberOfVertices, 5);
            Assert.AreEqual(stringGraph.NumberOfEdges, 3);
            Assert.AreEqual(stringGraph.Vertices.Length, 5);
            Assert.AreEqual(stringGraph.Edges.Length, 3);
        }

        [Test]
        public void ConstructorArrayEdgeDataClassGraph()
        {
            var array = classEdgeData.ToArray();
            classGraph = new Graph<ClassTest>(array[0], array[1], array[2]);

            Assert.AreEqual(classGraph.NumberOfVertices, 6);
            Assert.AreEqual(classGraph.NumberOfEdges, 3);
            Assert.AreEqual(classGraph.Vertices.Length, 6);
            Assert.AreEqual(classGraph.Edges.Length, 3);
        }

        # endregion

        # region Adding vertex

        [Test]
        public void AddVertexStringGraph()
        {
            Assert.AreEqual(stringGraph.NumberOfVertices, 0);
            Assert.AreEqual(stringGraph.Vertices.Length, 0);

            stringGraph.AddVertex("hello");

            Assert.AreEqual(stringGraph.NumberOfVertices, 1);
            Assert.AreEqual(stringGraph.Vertices.Length, 1);
        }

        [Test]
        public void AddVertexClassGraph()
        {
            Assert.AreEqual(classGraph.NumberOfVertices, 0);
            Assert.AreEqual(classGraph.Vertices.Length, 0);

            var klass = new ClassTest(20);
            classGraph.AddVertex(klass);

            Assert.AreEqual(classGraph.NumberOfVertices, 1);
            Assert.AreEqual(classGraph.Vertices.Length, 1);
        }

        # endregion

        # region Adding edge for existent nodes

        [Test]
        public void AddEdgeStringGraph()
        {
            stringGraph.AddVertex("me");
            stringGraph.AddVertex("you");

            Assert.AreEqual(stringGraph.NumberOfVertices, 2);
            Assert.AreEqual(stringGraph.Edges.Length, 0);

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
        public void AddEdgeWithoutVerticesStringGraph()
        {
            stringGraph.AddEdge("me", "you");

            Assert.AreEqual(stringGraph.NumberOfEdges, 1);
            Assert.AreEqual(stringGraph.NumberOfVertices, 2);
        }

        [Test]
        public void AddEdgeWtihoutVerticesClassGraph()
        {
            classGraph.AddEdge(new ClassTest(20), new ClassTest(15));

            Assert.AreEqual(classGraph.NumberOfVertices, 2);
            Assert.AreEqual(classGraph.NumberOfEdges, 1);
        }

        # endregion

        # region Adding path without vertex in graph

        [Test]
        public void AddPathNoVertexPresentStringGraph()
        {
            stringGraph.AddPath(stringData.ToArray());

            Assert.AreEqual(stringGraph.NumberOfVertices, stringData.Count);
            Assert.AreEqual(stringGraph.NumberOfEdges, stringData.Count - 1);
        }

        [Test]
        public void AddPathNoVertexPresentClassGraph()
        {
            classGraph.AddPath(classData);

            Assert.AreEqual(classGraph.NumberOfVertices, classData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, classData.Count - 1);
        }

        # endregion

        # region Adding path with some vertex in graph

        [Test]
        public void AddPathSomeVertexPresentStringGraph()
        {
            stringGraph.AddVertices("newer", "blackbox");

            Assert.AreEqual(stringGraph.NumberOfVertices, 2);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);

            stringGraph.AddPath(stringData);
            Assert.AreEqual(stringGraph.NumberOfVertices, stringData.Count);
            Assert.AreEqual(stringGraph.NumberOfEdges, stringData.Count - 1);
        }

        [Test]
        public void AddPathSomeVertexPresentClassGraph()
        {
            classGraph.AddVertices(classData);

            Assert.AreEqual(classGraph.NumberOfVertices, classData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);

            classGraph.AddPath(classData);
            Assert.AreEqual(classGraph.NumberOfVertices, classData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, classData.Count - 1);
        }

        # endregion

        # region Adding cycle without vertex in graph

        [Test]
        public void AddCycleNoVertexPresentStringGraph()
        {
            stringGraph.AddVertex("winterfell");

            Assert.AreEqual(stringGraph.NumberOfVertices, 1);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);

            stringGraph.AddCycle(stringData);

            Assert.AreEqual(stringGraph.NumberOfVertices, 1 + stringData.Count);
            Assert.AreEqual(stringGraph.NumberOfEdges, stringData.Count);
        }

        [Test]
        public void AddCycleNoVertexPresentClassGraph()
        {
            classGraph.AddCycle(classData);

            Assert.AreEqual(classGraph.NumberOfVertices, classData.Count);
            Assert.AreEqual(classGraph.NumberOfEdges, classData.Count);
        }

        # endregion

        # region Adding cycle with some vertex already present in graph

        [Test]
        public void AddCycleWithSomeVertexPresentStringGraph()
        {
            stringGraph.AddVertex(stringData[1]);

            Assert.AreEqual(stringGraph.NumberOfVertices, 1);
            Assert.AreEqual(stringGraph.NumberOfEdges, 0);

            stringGraph.AddCycle(stringData);

            Assert.AreEqual(stringGraph.NumberOfVertices, stringData.Count);
            Assert.AreEqual(stringGraph.NumberOfEdges, stringData.Count);
        }

        [Test]
        public void AddCycleWithSomeVertexPresentClassGraph()
        {
            classGraph.AddVertices(classData[1], classData[0], new ClassTest(9826593));


            Assert.AreEqual(classGraph.NumberOfVertices, 3);
            Assert.AreEqual(classGraph.NumberOfEdges, 0);

            classGraph.AddCycle(classData);

            Assert.AreEqual(classGraph.NumberOfVertices, stringData.Count + 1);
            Assert.AreEqual(classGraph.NumberOfEdges, stringData.Count);
        }

        # endregion

        # region Neighbors for vertex

        [Test]
        public void NeighborsForStringGraph()
        {
            stringGraph.AddEdges(stringEdgeData);
            var neighbors = stringGraph.Neighbors("fork");

            Assert.AreEqual(neighbors.Length, 2);
            CheckValues(neighbors, new []{"together", "invalid"});
        }

        [Test]
        public void NeighborsForClassGraph()
        {
            classGraph.AddEdges(classEdgeData);
            var neighbors = classGraph.Neighbors(classEdgeData[2].Second);

            Assert.AreEqual(neighbors.Length, 1);

            CheckValues(neighbors, new[] { classEdgeData[2].First });

        }

        # endregion

        # region Private Methods

        private void CheckValues<T>(T[] array1, T[] array2)
        {
            Assert.AreEqual(array1.Length, array2.Length);

            for (int i = 0; i < array1.Length; i++)
            {
                Assert.AreEqual(array1[i], array1[i]);
            }
        }

        # endregion

    }
}
