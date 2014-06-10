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
    }
}
