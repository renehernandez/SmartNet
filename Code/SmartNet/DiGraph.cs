using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class DiGraph<TVertex, TEdge> : BaseGraph<TVertex, TEdge>, IGraph<DiGraph<TVertex, TEdge>, TVertex, TEdge> 
        where TEdge : IEdge<TVertex> 
        where TVertex : IEquatable<TVertex>

    {
        public override void AddEdge(TEdge edge)
        {
            throw new NotImplementedException();
        }

        public DiGraph<TVertex, TEdge> Subgraph(IEnumerable<TVertex> vertices)
        {
            throw new NotImplementedException();
        }

        public DiGraph<TVertex, TEdge> Subgraph(params TVertex[] vertices)
        {
            throw new NotImplementedException();
        }

        public override void RemoveEdge(TVertex source, TVertex target)
        {
            throw new NotImplementedException();
        }
    }
}
