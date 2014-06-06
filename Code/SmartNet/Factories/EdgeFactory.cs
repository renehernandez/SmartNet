using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Factories
{
    public delegate TEdge EdgeFactory<in TVertex, out TEdge>(TVertex source, TVertex target)
        where TVertex : IEquatable<TVertex>
        where TEdge : IEdge<TVertex>;
}
