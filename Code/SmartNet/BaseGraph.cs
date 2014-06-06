using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.AccessControl;
using SmartNet.Exceptions;
using SmartNet.Factories;
using SmartNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet
{
    public abstract class BaseGraph<TVertex, TEdge> : IGraph<TVertex, TEdge>
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TVertex>
    {

        # region Private Fields

        protected Dictionary<TVertex, Dictionary<TVertex, TEdge>> adj;

        protected EdgeFactory<TVertex, TEdge> edgeFactory;

        # endregion

        # region Public Properties

        public int NumberOfVertices { get; protected set; }

        public int NumberOfEdges { get; protected set; }

        public TVertex[] Vertices
        {
            get { return VerticesIterator.ToArray(); }
        }

        public IEnumerable<TVertex> VerticesIterator
        {
            get { return adj.Keys.AsEnumerable(); }
        }

        public TEdge[] Edges 
        {
            get { return EdgesIterator.ToArray(); }
        }

        public IEnumerable<TEdge> EdgesIterator
        {
            get { return adj.Values.SelectMany(x => x.Values).Distinct(); }
        }
       
        public Dictionary<TVertex, TEdge> this[TVertex v]
        {
            get 
            {
                if (!HasVertex(v))
                    throw new VertexNotFoundException("Vertex {0} not found in graph", v);
                return adj[v]; 
            }
        }

        public TEdge this[TVertex source, TVertex target] 
        {
            get 
            {
                if (!HasVertex(source))
                    throw new EdgeNotFoundException("Vertex {0} not found in graph when looking for edge ({1}, {2}) ", source, source, target);
                if (!HasVertex(target))
                    throw new EdgeNotFoundException("Vertex {0} not found in graph when looking for edge ({1}, {2}) ", source, source, target);
                if(!adj[source].ContainsKey(target))
                    throw new EdgeNotFoundException("Edge ({0}, {1}) not found in graph", source, target);
                
                return adj[source][target]; 
            }
        }

        # endregion

        # region Constructors

        protected BaseGraph()
        {
            NumberOfEdges = 0;
            NumberOfVertices = 0;
            adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>();

            edgeFactory = GetFactory();
        }

        protected BaseGraph(IEnumerable<TVertex> vertices): this()
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        protected BaseGraph(params TVertex[] vertices):this(vertices.AsEnumerable())
        {
        }

        protected BaseGraph(IEnumerable<TEdge> edges):this()
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        protected BaseGraph(params TEdge[] edges):this(edges.AsEnumerable())
        {
        }

        protected BaseGraph(IEqualityComparer<TVertex> comparer):this()
        {
            adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>(comparer);
        }

        protected BaseGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TVertex> vertices)
            : this(vertices)
        {
            adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>(comparer);
        }

        protected BaseGraph(IEqualityComparer<TVertex> comparer, params TVertex[] vertices)
            : this(comparer, vertices.AsEnumerable())
        {
        }

        protected BaseGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TEdge> edges): this(edges)
        {
            adj = new Dictionary<TVertex, Dictionary<TVertex, TEdge>>(comparer);
        }

        protected BaseGraph(IEqualityComparer<TVertex> comparer, params TEdge[] edges)
            : this(comparer, edges.AsEnumerable())
        {
        }

        # endregion

        # region Public Methods

            # region Additions

        public void AddVertex(TVertex v)
        {
            if (adj.ContainsKey(v))
                throw new DuplicatedVertexException("Vertex {0} already found in graph", v);

            adj[v] = new Dictionary<TVertex, TEdge>();
            
            NumberOfVertices++;
        }

        public void AddVertices(IEnumerable<TVertex> vertexList)
        {
            foreach (var vertex in vertexList)
            {
                AddVertex(vertex);
            }
        }

        public void AddVertices(params TVertex[] vertices)
        {
            AddVertices(vertices.AsEnumerable());
        }

        public abstract void AddEdge(TEdge edge);

        public void AddEdge(TVertex source, TVertex target)
        {
            var edge = edgeFactory(source, target);
            AddEdge(edge);
        }

        public void AddEdge(Tuple<TVertex, TVertex> tuple)
        {
            AddEdge(tuple.Item1, tuple.Item2);
        }

        public void AddEdges(IEnumerable<TEdge> edges)
        {
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        public void AddEdges(params TEdge[] edges)
        {
            AddEdges(edges.AsEnumerable());
        }

        public void AddEdges(IEnumerable<Tuple<TVertex, TVertex>> edges)
        {
            foreach (var tuple in edges)
            {
                AddEdge(tuple.Item1, tuple.Item2);
            }
        }

        public void AddEdges(params Tuple<TVertex, TVertex>[] edges)
        {
            AddEdges(edges.AsEnumerable());
        }

        public void AddPath(IEnumerable<TVertex> vertices)
        {
            TVertex first = vertices.First();

            foreach (var second in vertices.Skip(1))
            {
                AddEdge(first, second);
                first = second;
            }
        }

        public void AddPath(params TVertex[] vertices)
        {
            AddPath(vertices.AsEnumerable());
        }

        public void AddCycle(IEnumerable<TVertex> vertices)
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

        public void AddCycle(params TVertex[] vertices)
        {
            AddCycle(vertices.AsEnumerable());
        }

            # endregion

            # region Degree Information

        public int Degree(TVertex vertex)
        {
            if (!HasVertex(vertex))
            {
                throw new VertexNotFoundException("Vertex {0} not found", vertex);
            }

            return adj[vertex].Count;
        }

        public IEnumerable<int> DegreesIterator(IEnumerable<TVertex> vertices)
        {
            return vertices.Select(Degree);
        }

        public IEnumerable<int> DegreesIterator(params TVertex[] vertices)
        {
            return DegreesIterator(vertices.AsEnumerable());
        }

        public int[] Degrees(IEnumerable<TVertex> vertices)
        {
            return DegreesIterator(vertices).ToArray();
        }

        public int[] Degrees(params TVertex[] vertices)
        {
            return DegreesIterator(vertices).ToArray();
        }

            # endregion

            # region Contains-like Information

        public bool HasVertex(TVertex v)
        {
            return adj.ContainsKey(v);
        }

        public bool HasEdge(TVertex source, TVertex target)
        {
            if (!HasVertex(source))
                return false;

            return adj[source].ContainsKey(target);
        }

        public bool HasEdge(TEdge edge)
        {
            return HasEdge(edge.Source, edge.Target);
        }

            # endregion

            # region Neighbors Search

        public IEnumerable<TVertex> NeighborsIterator(TVertex vertex)
        {
            if (!HasVertex(vertex))
                throw new VertexNotFoundException("Vertex {0} not found in graph", vertex);

            return adj[vertex].Keys.AsEnumerable();
        }

        public TVertex[] Neighbors(TVertex vertex)
        {
            return NeighborsIterator(vertex).ToArray();
        }

        public abstract IGraph<TVertex, TEdge> Subgraph(IEnumerable<TVertex> vertices);

        # endregion

            # region Subgraphs

        public IGraph<TVertex, TEdge> Subgraph(params TVertex[] vertices)
        {
            return Subgraph(vertices.AsEnumerable());
        }

        # endregion

        # region Adjacency List

        public IEnumerable<IEnumerable<TVertex>> AdjacencyListIterator()
        {
            return adj.Select(pair => pair.Value.Keys);
        }

        public TVertex[][] AdjacencyList()
        {
            return adj.Select(pair => pair.Value.Keys.ToArray()).ToArray();
        }

        # endregion


        # region Deletions

        public void Clear()
        {
            adj.Clear();

            NumberOfVertices = 0;
            NumberOfEdges = 0;
        }

        public void RemoveVertex(TVertex v)
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

        public void RemoveVertices(IEnumerable<TVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                RemoveVertex(vertex);
            }
        }

        public void RemoveVertices(params TVertex[] vertices)
        {
            RemoveVertices(vertices.AsEnumerable());
        }

        public abstract void RemoveEdge(TVertex source, TVertex target);

        public void RemoveEdge(TEdge edge)
        {
            RemoveEdge(edge.Source, edge.Target);
        }

        public void RemoveEdges(IEnumerable<TEdge> edges)
        {
            foreach (var edge in edges)
            {
                RemoveEdge(edge);
            }
        }

        public void RemoveEdges(params TEdge[] edges)
        {
            RemoveEdges(edges.AsEnumerable());
        }

            # endregion

        # endregion

        # region Private Methods

        private static EdgeFactory<TVertex, TEdge> GetFactory()
        {
            var type = typeof (TEdge);

            var ctor = type.GetConstructor(new [] {typeof (TVertex), typeof (TVertex)});

            ParameterInfo[] paramsInfo = ctor.GetParameters();

            //create a single param of type object[]
            ParameterExpression paramFirst = Expression.Parameter(typeof(TVertex));

            ParameterExpression paramSecond = Expression.Parameter(typeof (TVertex));

            Expression[] argsExp = new Expression[paramsInfo.Length];

            argsExp[0] = Expression.Convert(paramFirst, typeof (TVertex));
            argsExp[1] = Expression.Convert(paramSecond, typeof(TVertex));

            NewExpression newExp = Expression.New(ctor, argsExp);
            LambdaExpression lambda = Expression.Lambda(typeof(EdgeFactory<TVertex, TEdge>), newExp, paramFirst, paramSecond);

            return lambda.Compile() as EdgeFactory<TVertex, TEdge>;
        }

        # endregion

    }
}
