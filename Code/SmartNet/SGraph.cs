using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet
{
    public class SGraph<TVertex> : Graph<TVertex, SEdge<TVertex>, Data> 
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

    }
}
