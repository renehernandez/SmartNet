using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet
{
    public class SDiGraph<TVertex> : DiGraph<TVertex, SDiEdge<TVertex>, Data> 
        where TVertex : IEquatable<TVertex>
    {
    }
}
