using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class Edge<TVertex> : IEdge<TVertex> 
        where TVertex: IEquatable<TVertex>
    {

        # region Private Fields

        # endregion

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

        public Edge(TVertex source, TVertex target, double weight)
        {
            Source = source;
            target = target;
            Weight = weight;
        }

        public Edge(Tuple<TVertex, TVertex> tuple, double weight) : this(tuple.Item1, tuple.Item2, weight)
        {
            
        }

        public Edge(TVertex source, TVertex target): this(source, target, 1.0)
        {
        }

        public Edge(Tuple<TVertex, TVertex> tuple)
            : this(tuple.Item1, tuple.Item2, 1.0)
        {
        }

        # endregion

        # region Public Methods

        public override string ToString()
        {
            return string.Format("{0} <-> {1}", Source, Target);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Edge<TVertex>);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() + Target.GetHashCode();
        }

        public bool Equals(IEdge<TVertex> other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var edge = other as Edge<TVertex>;

            if (ReferenceEquals(edge, null))
            {
                return false;
            }


            return (Source.Equals(edge.Source) && Target.Equals(edge.Target)) || (
                Source.Equals(edge.Target) && Target.Equals(edge.Source));
        }

        public static bool operator ==(Edge<TVertex> left, Edge<TVertex> right)
        {
            return Object.Equals(left, right);
        }

        public static bool operator !=(Edge<TVertex> left, Edge<TVertex> right)
        {
            return !(left == right);
        }

        # endregion

        # region Private Methods

        # endregion


    }
}
