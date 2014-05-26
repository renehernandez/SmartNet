using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class Edge<T> : IEquatable<Edge<T>> where T : IEquatable<T> 
    {

        # region Private Fields

        # endregion

        # region Public Properties

        public T First { get; private set; }

        public T Second { get; private set; }

        public IContainer Data { get; set; }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > 1)
                    throw new IndexOutOfRangeException("Invalid index for edge object");
                return index == 0 ? First : Second;
            }
            set
            {
                if (index < 0 || index > 1)
                    throw new IndexOutOfRangeException("Invalid index for edge object");

                if (index == 0)
                    First = value;
                else
                    Second = value;
            }
        }

        # endregion

        # region Constructors

        public Edge(T left, T right){
            First = left;
            Second = right;
            Data = new BaseContainer();
        }

        public Edge(T left, T right, IContainer data): this(left, right) {
            Data = data;
        }

        public Edge(Tuple<T, T> tuple)
            : this(tuple.Item1, tuple.Item2)
        {
        }

        # endregion

        # region Public Methods

        public override string ToString()
        {
            return string.Format("({0}, {1})", First, Second);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Edge<T>);
        }

        public override int GetHashCode()
        {
            int hash = 17 * 31 + First.GetHashCode();
            return hash * 31 + Second.GetHashCode();
        }

        public bool Equals(Edge<T> other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return First.Equals(other.First) && Second.Equals(other.Second);
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
