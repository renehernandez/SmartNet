using SmartNet.Exceptions;
using SmartNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Utilities
{
    public class BinaryHeap<T> : IBinaryHeap<T> where T : IComparable<T>
    {

        # region Private Fields

        Func<T, T, int> comparer;

        # endregion

        # region Constructors

        public BinaryHeap()
        {
            comparer = (x, y) => x.CompareTo(y);
        }

        public BinaryHeap(IComparer<T> comparer)
        {
            this.comparer = comparer.Compare;
        }

        # endregion

        # region Public Methods

        public void BuildMinHeap(List<T> list)
        {
            for (int i = list.Count / 2 - 1; i >= 0; i--)
            {
                MinHeapify(list, i);
            }
        }

        public void BuildMaxHeap(List<T> list)
        {
            for (int i = list.Count / 2 - 1; i >= 0; i--)
            {
                MaxHeapify(list, i);
            }
        }

        public void HeapDecreaseKey(List<T> list, int index, T item)
        {
            if (comparer != null && comparer(item, list[index]) > 0)
                throw new InvalidItemComparisonForHeapException("New item is bigger than current item");

            list[index] = item;

            int parent = Parent(index);

            while (index > 0 && comparer(list[parent], list[index]) > 0)
            {
                T temp = list[parent];
                list[parent] = list[index];
                list[index] = temp;
                index = parent;
                parent = Parent(index);
            }
        }

        public void HeapIncreaseKey(List<T> list, int index, T item)
        {
            if (comparer != null && comparer(item, list[index]) < 0)
                throw new InvalidItemComparisonForHeapException("New item is smaller than current item");

            list[index] = item;
            
            int parent = Parent(index);

            while (index > 0 && comparer(list[parent], list[index]) < 0)
            {
                T temp = list[parent];
                list[parent] = list[index];
                list[index] = temp;
                index = parent;
                parent = Parent(index);
            }
        }

        public void MinHeapify(List<T> list, int index)
        {
            MinHeapify(list, index, list.Count);
        }

        public void MinHeapify(List<T> list, int index, int count)
        {
            int left = Left(index);
            int right = Right(index);

            int min = index;

            if (left < count && comparer(list[left], list[min]) < 0)
                min = left;

            if (right < count && comparer(list[right], list[min]) < 0)
                min = right;

            if (min != index)
            {
                T temp = list[index];
                list[index] = list[min];
                list[min] = temp;
                MinHeapify(list, min, count);
            }
        }

        public void MaxHeapify(List<T> list, int index)
        {
            MaxHeapify(list, index, list.Count);
        }

        public void MaxHeapify(List<T> list, int index, int count)
        {
            int left = Left(index);
            int right = Right(index);

            int max = index;

            if (left < count && comparer(list[left], list[max]) > 0)
                max = left;

            if (right < count && comparer(list[right], list[max]) > 0)
                max = right;

            if (max != index)
            {
                T temp = list[index];
                list[index] = list[max];
                list[max] = temp;
                MaxHeapify(list, max, count);
            }
        }

        # endregion

        # region Private Methods

        private int Parent(int index)
        {
            return (index - 1) / 2;
        }

        private int Left(int index)
        {
            return 2 * index + 1;
        }

        private int Right(int index)
        {
            return 2 * index + 2;
        }

        # endregion

    }
}
