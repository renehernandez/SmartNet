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
                if (!HasVertex(v))
                    throw new VertexNotFoundException("Vertex {0} not found in graph", v);
                return adj[v]; 
            }
        }

        public Edge<T> this[T v, T w] 
        {
            get 
            {
                if (!HasVertex(v))
                    throw new EdgeNotFoundException("Vertex {0} not found in graph when looking for edge ({1}, {2}) ", v, v, w);
                if (!HasVertex(w))
                    throw new EdgeNotFoundException("Vertex {0} not found in graph when looking for edge ({1}, {2}) ", v, v, w);
                if(!adj[v].ContainsKey(w))
                    throw new EdgeNotFoundException("Edge ({0}, {1}) not found in graph", v, w);
                
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

        public Graph(IEnumerable<T> vertices): this()
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        public Graph(params T[] vertices):this(vertices.AsEnumerable())
        {
        }

        public Graph(IEnumerable<Edge<T>> edges):this()
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public Graph(params Edge<T>[] edges):this(edges.AsEnumerable())
        {
        }
        
        public Graph(IEqualityComparer<T> comparer)
        {
            NumberOfEdges = 0;
            NumberOfVertices = 0;
            adj = new Dictionary<T, Dictionary<T, Edge<T>>>(comparer);
        }

        public Graph(IEqualityComparer<T> comparer, IEnumerable<T> vertices)
            : this(vertices)
        {
            adj = new Dictionary<T, Dictionary<T, Edge<T>>>(comparer);
        }

        public Graph(IEqualityComparer<T> comparer, params T[] vertices)
            : this(comparer, vertices.AsEnumerable())
        {
        }

        public Graph(IEqualityComparer<T> comparer, IEnumerable<Edge<T>> edges): this(edges)
        {
            adj = new Dictionary<T, Dictionary<T, Edge<T>>>(comparer);
        }

        public Graph(IEqualityComparer<T> comparer, params Edge<T>[] edges)
            : this(comparer, edges.AsEnumerable())
        {
        }

        # endregion

        # region Public Methods

            # region Additions

        public void AddVertex(T v)
        {
            if (adj.ContainsKey(v))
                throw new DuplicatedVertexException("Vertex {0} already found in graph", v);

            adj[v] = new Dictionary<T, Edge<T>>();
            
            NumberOfVertices++;
        }

        public void AddVertices(IEnumerable<T> vertexList)
        {
            foreach (var vertex in vertexList)
            {
                AddVertex(vertex);
            }
        }

        public void AddVertices(params T[] vertices)
        {
            AddVertices(vertices.AsEnumerable());
        }

        public void AddEdge(Edge<T> edge)
        {
            if (!adj.ContainsKey(edge.First))
                AddVertex(edge.First);
            if(!adj.ContainsKey(edge.Second))
                AddVertex(edge.Second);

            if (adj[edge.First].ContainsKey(edge.Second))
                throw new DuplicatedEdgeException("Edge {0} already present in graph", edge);

            adj[edge.First].Add(edge.Second, edge);
            adj[edge.Second].Add(edge.First, edge.Reverse());

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

        public void AddEdge(Tuple<T, T> tuple)
        {
            AddEdge(tuple.Item1, tuple.Item2);
        }

        public void AddEdge(Tuple<T, T> tuple, IContainer data)
        {
            AddEdge(tuple.Item1, tuple.Item2, data);
        }

        public void AddEdges(IEnumerable<Edge<T>> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddEdges(params Edge<T>[] edges)
        {
            AddEdges(edges.AsEnumerable());
        }

        public void AddEdges(IEnumerable<Tuple<T, T>> edges)
        {
            foreach (var tuple in edges)
            {
                AddEdge(tuple.Item1, tuple.Item2);
            }
        }

        public void AddEdges(params Tuple<T, T>[] edges)
        {
            AddEdges(edges.AsEnumerable());
        }

        public void AddPath(IEnumerable<T> vertices)
        {
            T first = vertices.First();

            foreach (var second in vertices.Skip(1))
            {
                AddEdge(first, second);
                first = second;
            }
        }

        public void AddPath(params T[] vertices)
        {
            AddPath(vertices.AsEnumerable());
        }

        public void AddCycle(IEnumerable<T> vertices)
        {
            T first = vertices.First();
            T current = first;

            foreach (var next in vertices.Skip(1))
            {
                AddEdge(current, next);
                current = next;
            }
            AddEdge(current, first);
        }

        public void AddCycle(params T[] vertices)
        {
            AddCycle(vertices.AsEnumerable());
        }

            # endregion

            # region Degree Information

        public int Degree(T vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new VertexNotFoundException("Vertex {0} not found", vertex);
            }

            return adj[vertex].Count;
        }

        public IEnumerable<int> DegreesIterator(IEnumerable<T> vertices)
        {
            return vertices.Select(v => Degree(v));
        }

        public IEnumerable<int> DegreesIterator(params T[] vertices)
        {
            return DegreesIterator(vertices.AsEnumerable());
        }

        public int[] Degrees(IEnumerable<T> vertices)
        {
            return DegreesIterator(vertices).ToArray();
        }

        public int[] Degrees(params T[] vertices)
        {
            return DegreesIterator(vertices).ToArray();
        }

            # endregion

            # region Contains-like Information

        public bool HasVertex(T v)
        {
            return adj.ContainsKey(v);
        }

        public bool HasEdge(T v, T w)
        {
            if (!HasVertex(v))
                return false;

            return adj[v].ContainsKey(w);
        }

        public bool HasEdge(Edge<T> edge)
        {
            return HasEdge(edge.First, edge.Second);
        }

            # endregion

            # region Neighbors Search

        public IEnumerable<T> NeighborsIterator(T vertex)
        {
            if (!HasVertex(vertex))
                throw new VertexNotFoundException("Vertex {0} not found in graph", vertex);

            return adj[vertex].Keys.AsEnumerable();
        }

        public T[] Neighbors(T vertex)
        {
            return NeighborsIterator(vertex).ToArray();
        }

            # endregion

            # region Subgraphs

        public Graph<T> Subgraph(IEnumerable<T> vertices)
        {
            var subgraph = new Graph<T>(vertices);

            subgraph.AddEdges(
                vertices.SelectMany(vertex => adj[vertex].Values.Where(
                    edge => subgraph.HasVertex(edge.Second) && !subgraph.HasEdge(vertex, edge.Second))
                )
                );

            return subgraph;
        }

        public Graph<T> Subgraph(params T[] vertices)
        {
            return Subgraph(vertices.AsEnumerable());
        }

        # endregion

        # region Deletions

        public void Clear()
        {
            adj.Clear();

            NumberOfVertices = 0;
            NumberOfEdges = 0;
        }

        public void RemoveVertex(T v)
        {
            if (!adj.ContainsKey(v))
                throw new VertexNotFoundException("Vertex {0} not found in graph", v);

            adj.Remove(v);

            foreach (var w in adj.Keys.Where(w => adj[w].ContainsKey(v)))
            {
                adj[w].Remove(v);
            }

            NumberOfVertices--;
        }

        public void RemoveVertices(IEnumerable<T> vertices)
        {
            foreach (var vertex in vertices)
            {
                RemoveVertex(vertex);
            }
        }

        public void RemoveVertices(params T[] vertices)
        {
            RemoveVertices(vertices.AsEnumerable());
        }

        public void RemoveEdge(T v, T w)
        {
            if (!adj.ContainsKey(v))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge ({1}, {2})", v, v, w);

            if (!adj.ContainsKey(w))
                throw new VertexNotFoundException("Vertex {0} not found in graph for removing edge ({1}, {2})", v, v, w);

            if (!adj[v].ContainsKey(w))
                throw new EdgeNotFoundException("Edge ({0}, {1}) not found in graph", v, w);

            adj[v].Remove(w);
            adj[w].Remove(v);

            NumberOfEdges--;
        }

        public void RemoveEdge(Edge<T> edge)
        {
            RemoveEdge(edge.First, edge.Second);
        }

        public void RemoveEdges(IEnumerable<Edge<T>> edges)
        {
            foreach (var edge in edges)
            {
                RemoveEdge(edge);
            }
        }

        public void RemoveEdges(params Edge<T>[] edges)
        {
            RemoveEdges(edges.AsEnumerable());
        }

            # endregion

        # endregion

        # region Private Methods

        # endregion

    }
}
