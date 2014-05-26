﻿using SmartNet.Exceptions;
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
                    throw new VertexNotFoundException("Vertex {0} not found in graph", v);
                return adj[v]; 
            }
        }

        public Edge<T> this[T v, T w] 
        {
            get 
            {
                if (!adj.ContainsKey(v))
                    throw new EdgeNotFoundException("Vertex {0} not found in graph when looking for edge ({1}, {2}) ", v, v, w);
                if (!adj.ContainsKey(w))
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

        public void AddVertex(T v)
        {
            if (adj.ContainsKey(v))
                throw new DuplicatedVertexException("Vertex {0} already found in graph", v);

            adj[v] = new Dictionary<T, Edge<T>>();
            
            NumberOfVertices++;
        }

        public void AddVerticesFrom(IEnumerable<T> vertexList)
        {
            foreach (var vertex in vertexList)
            {
                AddVertex(vertex);
            }
        }

        public void AddVerticesFrom(params T[] vertices)
        {
            AddVerticesFrom(vertices.AsEnumerable());
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

        public void AddEdgesFrom(IEnumerable<Edge<T>> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddEdgesFrom(params Edge<T>[] edges)
        {
            AddEdgesFrom(edges.AsEnumerable());
        }

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

        public void RemoveVertex(T v)
        {
            if (!adj.ContainsKey(v))
                throw new VertexNotFoundException("Vertex {0} not found in graph", v);

            adj.Remove(v);

            foreach (var w in adj.Keys)
            {
                if (adj[w].ContainsKey(v))
                {
                    adj[w].Remove(v);
                }
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

        # region Private Methods

        # endregion

    }
}
