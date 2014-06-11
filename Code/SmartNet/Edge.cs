﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class Edge<TVertex, TData> : IEdge<Edge<TVertex, TData>, TVertex, TData> 
        where TVertex: IEquatable<TVertex>
        where TData: IData, new()
    {

        # region Private Fields

        # endregion

        # region Public Properties

        public TVertex Source { get; private set; }

        public TVertex Target { get; private set; }

        public TData Data { get; private set; }

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

        public Edge(TVertex source, TVertex target, TData data)
        {
            Source = source;
            Target = target;
            Data = data;
        }

        public Edge(TVertex source, TVertex target, double weight)
            : this(source, target, new TData() { Weight = weight})
        {
        }

        public Edge(TVertex source, TVertex target) : this(source, target, 1.0)
        {
        } 

        public Edge(Tuple<TVertex, TVertex> tuple, double weight) : this(tuple.Item1, tuple.Item2, weight)
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
            return string.Format("{0} <-> {1}, Data: {2}", Source, Target, Data);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Edge<TVertex, TData>);
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() + Target.GetHashCode();
        }

        public bool Equals(Edge<TVertex, TData> other)
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

        public static bool operator ==(Edge<TVertex, TData> left, Edge<TVertex, TData> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Edge<TVertex, TData> left, Edge<TVertex, TData> right)
        {
            return !(left == right);
        }

        # endregion

        # region Private Methods

        # endregion


    }
}
