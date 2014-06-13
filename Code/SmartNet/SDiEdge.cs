using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class SDiEdge<TVertex> : DiEdge<TVertex, EdgeData>, IEdge<SDiEdge<TVertex>, TVertex, EdgeData> 
        where TVertex : IEquatable<TVertex>
    {

        # region Constructors

        public SDiEdge(TVertex source, TVertex target, EdgeData data) : base(source, target, data)
        {
        }

        public SDiEdge(TVertex source, TVertex target, double weight): base(source, target, weight)
        {
        }

        public SDiEdge(TVertex source, TVertex target) : base(source, target, 1.0)
        {
        } 

        public SDiEdge(Tuple<TVertex, TVertex> tuple, double weight) : base(tuple.Item1, tuple.Item2, weight)
        {
        }

        public SDiEdge(Tuple<TVertex, TVertex> tuple) : base(tuple.Item1, tuple.Item2, 1.0)
        {
        }

        # endregion

        # region Public Methods

        public bool Equals(SDiEdge<TVertex> other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Source.Equals(other.Source) && Target.Equals(other.Target);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SDiEdge<TVertex>);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() + Target.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", Source, Target);
        }

        public static bool operator ==(SDiEdge<TVertex> first, SDiEdge<TVertex> second)
        {
            return Equals(first, second);
        }

        public static bool operator !=(SDiEdge<TVertex> first, SDiEdge<TVertex> second)
        {
            return !(first == second);
        }

        # endregion

    }
}
