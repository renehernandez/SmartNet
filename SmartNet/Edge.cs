using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class Edge<T> where T : IEquatable<T>
    {

        # region Private Fields

        # endregion

        # region Public Properties

        public T First { get; set; }

        public T Second { get; set; }

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

        # endregion

        # region Private Methods

        # endregion


    }
}
