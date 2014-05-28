using SmartNet.Exceptions;
using SmartNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Utilities
{
    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
    {

        # region Private Fields

        private List<T> list;       

        private IBinaryHeap<T> heap;

        # endregion

        # region Public Properties

        public int Count { get; private set; }

        # endregion

        # region Constructors

        public PriorityQueue()
        {
            list = new List<T>();
            heap = new BinaryHeap<T>();
            Count = 0;
        }

        public PriorityQueue(IComparer<T> comparer)
        {
            list = new List<T>();
            Count = 0;
            heap = new BinaryHeap<T>(comparer);
        }

        public PriorityQueue(IBinaryHeap<T> heap)
        {
            list = new List<T>();
            Count = 0;
            this.heap = heap;
        }

        public PriorityQueue(int capacity)
        {
            list = new List<T>(capacity);
            Count = 0;
            heap = new BinaryHeap<T>();
        }

        public PriorityQueue(IComparer<T> comparer, int capacity)
        {
            list = new List<T>(capacity);
            Count = 0;
            heap = new BinaryHeap<T>(comparer);
        }

        public PriorityQueue(IBinaryHeap<T> heap, int capacity)
        {
            list = new List<T>(capacity);
            Count = 0;
            this.heap = heap;
        }

        public PriorityQueue(IEnumerable<T> items)
        {
            list = new List<T>(items);
            heap = new BinaryHeap<T>();
            Count = list.Count;

            heap.BuildMaxHeap(list);
        }

        public PriorityQueue(params T[] items)
            : this(items.AsEnumerable())
        {
        }

        public PriorityQueue(IComparer<T> comparer, IEnumerable<T> items)
        {
            list = new List<T>(items);
            heap = new BinaryHeap<T>(comparer);
            Count = list.Count;

            heap.BuildMaxHeap(list);
        }

        public PriorityQueue(IComparer<T> comparer, params T[] items)
            : this(comparer, items.AsEnumerable())
        {
        }

        public PriorityQueue(IBinaryHeap<T> heap, IEnumerable<T> items)
        {
            list = new List<T>(items);
            this.heap = heap;
            Count = list.Count;

            this.heap.BuildMaxHeap(list);
        }

        public PriorityQueue(IBinaryHeap<T> heap, params T[] items):this(heap, items.AsEnumerable())
        {
        }

        # endregion

        # region Public Methods

        public void Clear()
        {
            Count = 0;
        }

        public T Dequeue()
        {
            if (IsEmpty())
                throw new PriorityQueueEmptyException("PriorityQueue is empty");

            T max = list[0];
            list[0] = list[Count - 1];
            Count--;

            heap.MaxHeapify(list, 0, Count);
            return max;
        }

        public IEnumerable<T> Dequeue(int count)
        {
            if (count <= 0)
                throw new DequeueNonPositiveQuantityException("Cannot be extracted zero or less elements");
 
            if (IsEmpty())
                throw new PriorityQueueEmptyException("PriorityQueue is empty");

            if(count > Count)
                throw new QuantityNotStoredQueueException("There {2} not {0} element{1} available", 
                    count, (count == 1 ? "" : "s"), (count == 1 ? "is" : "are"));

            return InnerDequeue(count);
        }

        private IEnumerable<T> InnerDequeue(int count)
        {
            for (int i = count; i > 0; i--)
                yield return Dequeue();
        }

        public void Enqueue(T item)
        {
            if (item == null)
                throw new NullElementEnqueueException("Element cannot be null due to operations in PriorityQueue class");

            if (Count < list.Count)
                list[Count] = item;
            else
                list.Add(item);

            Count++;

            heap.HeapIncreaseKey(list, Count - 1, item);
        }

        public void Enqueue(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Enqueue(item);
            }
        }

        public void Enqueue(params T[] items)
        {
            Enqueue(items.AsEnumerable());
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public T Peek()
        {
            if(IsEmpty())
                throw new PriorityQueueEmptyException("Priority Queue has not remaining elements");

            return list[0];
        }

        # endregion


    }
}
