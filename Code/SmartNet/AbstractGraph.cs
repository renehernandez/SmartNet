using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Exceptions;
using SmartNet.Factories;
using SmartNet.Interfaces;

namespace SmartNet
{
    public abstract class AbstractGraph<TVertex, TEdge, TGraphData, TEdgeData> 
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TEdge, TVertex, TEdgeData>
        where TEdgeData : IEdgeData, new()
        where TGraphData : IGraphData, new()
    {

        # region Private Fields

        protected Dictionary<TVertex, Dictionary<TVertex, TEdge>> Adj;

        protected EdgeFromVerticesFactory<TVertex, TEdgeData, TEdge> EdgeVerticesFactory;

        protected ReverseEdgeFactory<TVertex, TEdgeData, TEdge> ReverseEdgeFactory;

        # endregion

        # region Public Properties

        public virtual TGraphData Data { get; protected set; }

        public virtual int NumberOfVertices { get; protected set; }

        public virtual int NumberOfEdges { get; protected set; }

        public virtual TVertex[] Vertices
        {
            get { return VerticesIterator.ToArray(); }
        }

        public virtual IEnumerable<TVertex> VerticesIterator
        {
            get { return Adj.Keys.AsEnumerable(); }
        }

        public virtual TEdge[] Edges
        {
            get { return EdgesIterator.ToArray(); }
        }

        public virtual IEnumerable<TEdge> EdgesIterator
        {
            get { return Adj.Values.SelectMany(x => x.Values).Distinct(); }
        }

        public virtual Dictionary<TVertex, TEdge> this[TVertex v]
        {
            get
            {
                if (!HasVertex(v))
                    throw new VertexNotFoundException("Vertex {0} not found in graph", v);
                return Adj[v];
            }
        }

        public virtual TEdge this[TVertex source, TVertex target]
        {
            get
            {
                if (!HasVertex(source))
                    throw new EdgeNotFoundException("Vertex {0} not found in graph when looking for edge ({1}, {2}) ", source, source, target);
                if (!HasVertex(target))
                    throw new EdgeNotFoundException("Vertex {0} not found in graph when looking for edge ({1}, {2}) ", source, source, target);
                if (!Adj[source].ContainsKey(target))
                    throw new EdgeNotFoundException("Edge ({0}, {1}) not found in graph", source, target);

                return Adj[source][target];
            }
        }

        # endregion

        # region Constructors

        protected AbstractGraph()
        {
            NumberOfEdges = 0;
            NumberOfVertices = 0;
            Adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>();
            Data = new TGraphData();

            ReverseEdgeFactory = EdgeCreationFactory.GetReverseEdge<TVertex, TEdge, TEdgeData>();
            EdgeVerticesFactory = EdgeCreationFactory.GetEdgeFromVertices<TVertex, TEdge, TEdgeData>();
        }

        protected AbstractGraph(IEnumerable<TVertex> vertices)
            : this()
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        protected AbstractGraph(params TVertex[] vertices)
            : this(vertices.AsEnumerable())
        {
        }

        protected AbstractGraph(IEnumerable<TEdge> edges)
            : this()
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        protected AbstractGraph(params TEdge[] edges)
            : this(edges.AsEnumerable())
        {
        }

        protected AbstractGraph(IEqualityComparer<TVertex> comparer)
            : this()
        {
            Adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>(comparer);
        }

        protected AbstractGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TVertex> vertices)
            : this(vertices)
        {
            Adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>(comparer);
        }

        protected AbstractGraph(IEqualityComparer<TVertex> comparer, params TVertex[] vertices)
            : this(comparer, vertices.AsEnumerable())
        {
        }

        protected AbstractGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TEdge> edges)
            : this(edges)
        {
            Adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>(comparer);
        }

        protected AbstractGraph(IEqualityComparer<TVertex> comparer, params TEdge[] edges)
            : this(comparer, edges.AsEnumerable())
        {
        }

        # endregion

        # region Public Methods

        # region Additions

        public virtual void AddVertex(TVertex v)
        {
            if (Adj.ContainsKey(v))
                throw new DuplicatedVertexException("Vertex {0} already found in graph", v);

            Adj[v] = new Dictionary<TVertex, TEdge>();

            NumberOfVertices++;
        }

        public virtual void AddVertices(IEnumerable<TVertex> vertexList)
        {
            foreach (var vertex in vertexList)
            {
                AddVertex(vertex);
            }
        }

        public virtual void AddVertices(params TVertex[] vertices)
        {
            AddVertices(vertices.AsEnumerable());
        }

        public abstract void AddEdge(TEdge edge);

        public virtual void AddEdge(TVertex source, TVertex target)
        {
            var edge = EdgeVerticesFactory(source, target);
            AddEdge(edge);
        }

        public virtual void AddEdge(Tuple<TVertex, TVertex> tuple)
        {
            AddEdge(tuple.Item1, tuple.Item2);
        }

        public virtual void AddEdges(IEnumerable<TEdge> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public virtual void AddEdges(params TEdge[] edges)
        {
            AddEdges(edges.AsEnumerable());
        }

        public virtual void AddEdges(IEnumerable<Tuple<TVertex, TVertex>> edges)
        {
            foreach (var tuple in edges)
            {
                AddEdge(tuple.Item1, tuple.Item2);
            }
        }

        public virtual void AddEdges(params Tuple<TVertex, TVertex>[] edges)
        {
            AddEdges(edges.AsEnumerable());
        }

        public virtual void AddPath(IEnumerable<TVertex> vertices)
        {
            TVertex first = vertices.First();

            foreach (var second in vertices.Skip(1))
            {
                AddEdge(first, second);
                first = second;
            }
        }

        public virtual void AddPath(params TVertex[] vertices)
        {
            AddPath(vertices.AsEnumerable());
        }

        public virtual void AddCycle(IEnumerable<TVertex> vertices)
        {
            TVertex first = vertices.First();
            TVertex current = first;

            foreach (var next in vertices.Skip(1))
            {
                AddEdge(current, next);
                current = next;
            }
            AddEdge(current, first);
        }

        public virtual void AddCycle(params TVertex[] vertices)
        {
            AddCycle(vertices.AsEnumerable());
        }

        # endregion

        # region Degree Information

        public virtual int Degree(TVertex vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new VertexNotFoundException("Vertex {0} not found", vertex);
            }

            return Adj[vertex].Count;
        }

        public virtual IEnumerable<int> DegreesIterator(IEnumerable<TVertex> vertices)
        {
            return vertices.Select(Degree);
        }

        public virtual IEnumerable<int> DegreesIterator(params TVertex[] vertices)
        {
            return DegreesIterator(vertices.AsEnumerable());
        }

        public virtual int[] Degrees(IEnumerable<TVertex> vertices)
        {
            return DegreesIterator(vertices).ToArray();
        }

        public virtual int[] Degrees(params TVertex[] vertices)
        {
            return DegreesIterator(vertices).ToArray();
        }

        # endregion

        # region Contains-like Information

        public virtual bool HasVertex(TVertex v)
        {
            return Adj.ContainsKey(v);
        }

        public virtual bool HasEdge(TVertex source, TVertex target)
        {
            return HasVertex(source) && Adj[source].ContainsKey(target);
        }

        public virtual bool HasEdge(TEdge edge)
        {
            return HasEdge(edge.Source, edge.Target);
        }

        # endregion

        # region Neighbors Search

        public virtual IEnumerable<TVertex> NeighborsIterator(TVertex vertex)
        {
            if (!HasVertex(vertex))
                throw new VertexNotFoundException("Vertex {0} not found in graph", vertex);

            return Adj[vertex].Keys.AsEnumerable();
        }

        public virtual TVertex[] Neighbors(TVertex vertex)
        {
            return NeighborsIterator(vertex).ToArray();
        }

        public virtual IEnumerable<TEdge> NeighborsEdgesIterator(TVertex vertex)
        {
            return Adj[vertex].Values;
        }

        public virtual TEdge[] NeighborsEdges(TVertex vertex)
        {
            return NeighborsEdgesIterator(vertex).ToArray();
        }

        # endregion

        # region Adjacency List

        public virtual IEnumerable<KeyValuePair<TVertex, Dictionary<TVertex, TEdge>>> AdjacencyIterator()
        {
            return Adj.Select(pair => pair);
        }

        public virtual KeyValuePair<TVertex, Dictionary<TVertex, TEdge>>[] AdjacencyList()
        {
            return AdjacencyIterator().ToArray();
        }

        # endregion

        # region Deletions

        public virtual void Clear()
        {
            Adj.Clear();

            NumberOfVertices = 0;
            NumberOfEdges = 0;
        }

        public virtual void RemoveVertex(TVertex v)
        {
            if (!Adj.ContainsKey(v))
                throw new VertexNotFoundException("Vertex {0} not found in graph", v);

            Adj.Remove(v);

            foreach (var w in Adj.Keys.Where(w => Adj[w].ContainsKey(v)))
            {
                Adj[w].Remove(v);
            }

            NumberOfVertices--;
        }

        public virtual void RemoveVertices(IEnumerable<TVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                RemoveVertex(vertex);
            }
        }

        public virtual void RemoveVertices(params TVertex[] vertices)
        {
            RemoveVertices(vertices.AsEnumerable());
        }

        public abstract void RemoveEdge(TVertex source, TVertex target);

        public virtual void RemoveEdge(TEdge edge)
        {
            RemoveEdge(edge.Source, edge.Target);
        }

        public virtual void RemoveEdges(IEnumerable<TEdge> edges)
        {
            foreach (var edge in edges)
            {
                RemoveEdge(edge);
            }
        }

        public virtual void RemoveEdges(params TEdge[] edges)
        {
            RemoveEdges(edges.AsEnumerable());
        }

        # endregion

        # region Subgraph
        
        // These methods are implemented in derive classes

        # endregion

        # endregion

    }
}
