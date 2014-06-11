using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class SEdge<TVertex> : Edge<TVertex, Data>, IEdge<SEdge<TVertex>, TVertex, Data> 
        where TVertex : IEquatable<TVertex>
    {

          # region Constructors

        public SEdge(TVertex source, TVertex target, Data data) : base(source, target, data)
        {
        }

        public SEdge(TVertex source, TVertex target, double weight):base(source, target, weight)
        {
        }

        public SEdge(TVertex source, TVertex target) : base(source, target, 1.0)
        {
        } 

        public SEdge(Tuple<TVertex, TVertex> tuple, double weight) : base(tuple.Item1, tuple.Item2, weight)
        {
        }

        public SEdge(Tuple<TVertex, TVertex> tuple) : base(tuple.Item1, tuple.Item2, 1.0)
        {
        }

        # endregion

        # region Public Methods

        public override string ToString()
        {
            return string.Format("{0} <-> {1}, Data: {2}", Source, Target, Data);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SEdge<TVertex>);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() + Target.GetHashCode();
        }

        public bool Equals(SEdge<TVertex> other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return (Source.Equals(other.Source) && Target.Equals(other.Target)) || (
                Source.Equals(other.Target) && Target.Equals(other.Source));
        }

        public static bool operator ==(SEdge<TVertex> left, SEdge<TVertex> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SEdge<TVertex> left, SEdge<TVertex> right)
        {
            return !(left == right);
        }

        # endregion

    }
}
