using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class SGraph<TVertex> : Graph<TVertex, SEdge<TVertex>, GraphData, EdgeData>, IGraph<SGraph<TVertex>, TVertex, SEdge<TVertex>, GraphData, EdgeData> 
        where TVertex : IEquatable<TVertex>
    {

        # region Constructors

        public SGraph() : base()
        {
        }

        public SGraph(IEnumerable<TVertex> vertices): base(vertices)
        {
        }

        public SGraph(params TVertex[] vertices) : base(vertices)
        {
        }

        public SGraph(IEnumerable<SEdge<TVertex>> edges) : base(edges)
        {
        }

        public SGraph(params SEdge<TVertex>[] edges) : base(edges)
        {
        }

        public SGraph(IEqualityComparer<TVertex> comparer) : base(comparer)
        {
        }

        public SGraph(IEqualityComparer<TVertex> comparer, IEnumerable<TVertex> vertices)
            : base(comparer, vertices)
        {
        }

        public SGraph(IEqualityComparer<TVertex> comparer, params TVertex[] vertices)
            : base(comparer, vertices)
        {
        }

        public SGraph(IEqualityComparer<TVertex> comparer, IEnumerable<SEdge<TVertex>> edges)
            : base(comparer, edges)
        {
        }

        public SGraph(IEqualityComparer<TVertex> comparer, params SEdge<TVertex>[] edges)
            : base(comparer, edges)
        {
        }

        # endregion

        # region Public Methods

        public new SGraph<TVertex> Subgraph(IEnumerable<TVertex> vertices)
        {
            var listVertices = vertices.ToList();
            var subgraph = new SGraph<TVertex>(listVertices);

            subgraph.AddEdges(
                listVertices.SelectMany(vertex => Adj[vertex].Values.Where(
                    edge => subgraph.HasVertex(edge.Target) && !subgraph.HasEdge(vertex, edge.Target))
                )
                );

            return subgraph;
        }

        public new SGraph<TVertex> Subgraph(params TVertex[] vertices)
        {
            return Subgraph(vertices.AsEnumerable());
        }

        # endregion
    }
}
