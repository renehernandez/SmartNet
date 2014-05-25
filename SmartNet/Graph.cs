using SmartNet.Exceptions;
using SmartNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet
{
    public class Graph<T> where T : IEquatable<T>
    {

        # region Private Fields

        private Dictionary<T, Dictionary<T, Edge<T>>> adj;

        # endregion

        # region Public Properties
        public int NumberOfVertices { get; private set; }

        public int NumberOfEdges { get; private set; }

        public T[] Vertices
        {
            get { return VerticesIterator.ToArray(); }
        }

        public IEnumerable<T> VerticesIterator
        {
            get { return adj.Keys.AsEnumerable(); }
        }

        public Edge<T>[] Edges 
        {
            get { return EdgesIterator.ToArray(); }
        }

        public IEnumerable<Edge<T>> EdgesIterator
        {
            get { return adj.Values.SelectMany(x => x.Values).Distinct(); }
        }
       
        public Dictionary<T, Edge<T>> this[T v]
        {
            get 
            {
                if (!adj.ContainsKey(v))
                    throw new VertexNotFoundException(string.Format("Vertex {0} not found in graph", v));
                return adj[v]; 
            }
        }

        public Edge<T> this[T v, T w] 
        {
            get 
            {
                if (!adj.ContainsKey(v))
                    throw new EdgeNotFoundException(string.Format("Vertex {0} not found in graph", v));
                if(!adj[v].ContainsKey(w))
                    throw new Exception(string.Format("Vertex {0} not found in graph", w));
                
                return adj[v][w]; 
            }
        }

        # endregion

        # region Constructors

        public Graph()
        {
            NumberOfEdges = 0;
            NumberOfVertices = 0;
            adj = new Dictionary<T, Dictionary<T, Edge<T>>>();
        }

        public Graph(IEqualityComparer<T> comparer)
        {
            NumberOfEdges = 0;
            NumberOfVertices = 0;
            adj = new Dictionary<T, Dictionary<T, Edge<T>>>(comparer);
        }

        public Graph(params T[] vertices)
        {
        }

        public Graph(params Edge<T>[] edges)
        {

        }

        # endregion

        # region Public Methods

        public void AddVertex(T v)
        {
            if (adj.ContainsKey(v))
                throw new Exception(string.Format("Vertex {0} already present in graph", v));

            adj[v] = new Dictionary<T, Edge<T>>();
            
            NumberOfVertices++;
        }

        public void AddVertexFrom(IEnumerable<T> vertexList)
        {
            foreach (var vertex in vertexList)
            {
                AddVertex(vertex);
            }
        }

        public void AddEdge(Edge<T> edge)
        {
            if (!adj.ContainsKey(edge.First))
                AddVertex(edge.First);
            if(!adj.ContainsKey(edge.Second))
                AddVertex(edge.Second);

            if (adj[edge.First].ContainsKey(edge.Second))
                throw new Exception(string.Format("Edge {0} already present in graph", edge));

            adj[edge.First].Add(edge.Second, edge);
            adj[edge.Second].Add(edge.First, edge);

            NumberOfEdges++;
        }

        public void AddEdge(T v, T w)
        {
            var edge = new Edge<T>(v, w);
            AddEdge(edge);
        }

        public void AddEdge(T v, T w, IContainer data)
        {
            var edge = new Edge<T>(v, w, data);
            AddEdge(edge);
        }

        public bool HasVertex(T v)
        {
            return adj.ContainsKey(v);
        }

        public bool HasEdge(T v, T w)
        {
            return adj[v].ContainsKey(w);
        }

        # endregion

        # region Private Methods

        # endregion

    }
}
