using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Interfaces
{
    public interface IBinaryHeap<T> where T: IComparable<T>
    {

        void HeapIncreaseKey(List<T> list, int index, T value);

        void HeapDecreaseKey(List<T> list, int index, T value);

        void MinHeapify(List<T> list, int index);

        void MinHeapify(List<T> list, int index, int count);

        void MaxHeapify(List<T> list, int index);

        void MaxHeapify(List<T> list, int index, int count);

        void BuildMinHeap(List<T> list);

        void BuildMaxHeap(List<T> list);

    }
}
