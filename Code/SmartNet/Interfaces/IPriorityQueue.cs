using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Interfaces
{
    public interface IPriorityQueue<T> where T: IComparable<T>
    {

        int Count { get; }

        void Clear();

        T Dequeue();

        IEnumerable<T> Dequeue(int count);

        void Enqueue(T item);

        void Enqueue(IEnumerable<T> items);

        void Enqueue(params T[] items);

        bool IsEmpty();

        T Peek();

    }
}
