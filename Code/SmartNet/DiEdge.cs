using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class DiEdge<TVertex> : IEdge<DiEdge<TVertex>, TVertex> 
        where TVertex : IEquatable<TVertex>
    {

        # region Public Properties

        public TVertex Source { get; private set; }

        public TVertex Target { get; private set; }

        public double Weight { get; set; }

        public TVertex this[int index]
        {
            get
            {
                if (index < 0 || index > 1)
                    throw new IndexOutOfRangeException("Invalid index for edge object");
                return index == 0 ? Source : Target;
            }
        }

        # endregion

        # region Constructors

        public DiEdge(TVertex source, TVertex target, double weight)
        {
            Source = source;
            Target = target;
            Weight = weight;
        }

        public DiEdge(Tuple<TVertex, TVertex> tuple, double weight) : this(tuple.Item1, tuple.Item2, weight)
        {
        }

        public DiEdge(TVertex source, TVertex target): this(source, target, 1.0)
        {
        }

        public DiEdge(Tuple<TVertex, TVertex> tuple)
            : this(tuple.Item1, tuple.Item2, 1.0)
        {
        }


        # endregion

        # region Public Methods

        public bool Equals(DiEdge<TVertex> other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Source.Equals(other.Source) && Target.Equals(other.Target);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DiEdge<TVertex>);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() + Target.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} -> {1}", Source, Target);
        }

        public static bool operator ==(DiEdge<TVertex> first, DiEdge<TVertex> second)
        {
            return Equals(first, second);
        }

        public static bool operator !=(DiEdge<TVertex> first, DiEdge<TVertex> second)
        {
            return !(first == second);
        }

        # endregion
    }
}
