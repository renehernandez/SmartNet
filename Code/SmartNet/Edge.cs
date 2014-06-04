using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class Edge<T> : IEdge<T> where T: IEquatable<T>
    {

        # region Private Fields

        # endregion

        # region Public Properties

        public T Source { get; private set; }

        public T Target { get; private set; }

        public double Weight { get; set; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > 1)
                    throw new IndexOutOfRangeException("Invalid index for edge object");
                return index == 0 ? Source : Target;
            }
            set
            {
                if (index < 0 || index > 1)
                    throw new IndexOutOfRangeException("Invalid index for edge object");

                if (index == 0)
                    Source = value;
                else
                    Target = value;
            }
        }

        # endregion

        # region Constructors

        public Edge(T left, T right){
            Source = left;
            Target = right;
            Weight = 1.0;
        }

        public Edge(Tuple<T, T> tuple)
            : this(tuple.Item1, tuple.Item2)
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
            return Equals(obj as Edge<T>);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() + Target.GetHashCode();
        }

        public bool Equals(IEdge<T> other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var edge = other as Edge<T>;

            if (ReferenceEquals(edge, null))
            {
                return false;
            }


            return (Source.Equals(edge.Source) && Target.Equals(edge.Target)) || (
                Source.Equals(edge.Target) && Target.Equals(edge.Source));
        }

        public static bool operator ==(Edge<T> left, Edge<T> right)
        {
            return Object.Equals(left, right);
        }

        public static bool operator !=(Edge<T> left, Edge<T> right)
        {
            return !(left == right);
        }

        # endregion

        # region Private Methods

        # endregion


    }
}
