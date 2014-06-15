using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class SDiGraph<TVertex> : DiGraph<TVertex, SDiEdge<TVertex>, GraphData, EdgeData>, IGraph<SDiGraph<TVertex>, TVertex, SDiEdge<TVertex>, GraphData, EdgeData> 
        where TVertex : IEquatable<TVertex>
    {

        # region Constructors

        public SDiGraph() :base()
        {
        }

        public SDiGraph(IEnumerable<TVertex> vertices): base(vertices)
        {
        }

        public SDiGraph(params TVertex[] vertices) : base(vertices)
        {
        }

        public SDiGraph(IEnumerable<SDiEdge<TVertex>> edges) : base(edges)
        {
        }

        public SDiGraph(params SDiEdge<TVertex>[] edges) : base(edges)
        {
        }

        public SDiGraph(IEqualityComparer<TVertex> comparer) : base(comparer)
        {
        }

        public SDiGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TVertex> vertices)
            : base(comparer, vertices)
        {
        }

        public SDiGraph(IEqualityComparer<TVertex> comparer, params TVertex[] vertices)
            : base(comparer, vertices)
        {
        }

        public SDiGraph(IEqualityComparer<TVertex> comparer, IEnumerable<SDiEdge<TVertex>> edges)
            : base(comparer, edges)
        {
        }

        public SDiGraph(IEqualityComparer<TVertex> comparer, params SDiEdge<TVertex>[] edges)
            : base(comparer, edges)
        {
        }

        # endregion

        # region Public Methods

        public new SDiGraph<TVertex> Subgraph(IEnumerable<TVertex> vertices)
        {
            var listVertices = vertices.ToList();
            var subgraph = new SDiGraph<TVertex>(listVertices);

            subgraph.AddEdges(
                listVertices.SelectMany(vertex => Adj[vertex].Values.Where(
                    edge => subgraph.HasVertex(edge.Target) && !subgraph.HasEdge(vertex, edge.Target))
                )
                );

            return subgraph;
        }

        public new SDiGraph<TVertex> Subgraph(params TVertex[] vertices)
        {
            return Subgraph(vertices.AsEnumerable());
        }

        # endregion
    }
}
