using SmartNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.ConsoleTest
{

    public class BinaryJavaHeap<T> where T : IComparable<T>
    {

        public List<T> h = new List<T>();

        public BinaryJavaHeap()
        {
        }

        // building heap in O(n)
        public BinaryJavaHeap(T[] keys)
        {

            foreach (T key in keys)
            {
                h.Add(key);
            }

            for (int pos = h.Count / 2 - 1; pos >= 0; pos--)
            {
                moveDown(pos);
            }
        }

        public void add(T node)
        {
            h.Add(node);
            moveUp(h.Count() - 1);
        }

        void moveUp(int pos)
        {
            while (pos > 0)
            {
                int parent = (pos - 1) / 2;
                if (h[pos].CompareTo(h[parent]) >= 0)
                {
                    break;
                }
                T temp = h[pos];
                h[pos] = h[parent];
                h[parent] = temp;
                pos = parent;
            }
        }

        public T remove()
        {
            T removedNode = h[0];
            T lastNode = h[h.Count - 1];
            h.RemoveAt(h.Count - 1);
            if (h.Count != 0)
            {
                h[0] = lastNode;
                moveDown(0);
            }
            return removedNode;
        }

        void moveDown(int pos) {
            while (pos < h.Count / 2) {
                int child = 2 * pos + 1;
                if (child < h.Count - 1 && h[child].CompareTo(h[child + 1]) > 0) {
                    ++child;
                }
                if (h[pos].CompareTo(h[child]) <= 0) {
                    break;
                }
                T temp = h[pos];
                h[pos] = h[child];
                h[child] = temp;
                pos = child;
            }
        }
    }

    class Program
    {

        static void MaxHeapSort()
        {
            var heap = new BinaryHeap<int>();
            var list = new List<int>() { 25, 1351, -52, -7861234, 364, 38 };

            int[] array = new int[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            heap.BuildMinHeap(list);
            Console.WriteLine(list[0]);

            int count = list.Count;

            for (int i = list.Count - 1; i > 0; i--)
            {
                var temp = list[i];
                list[i] = list[0];
                list[0] = temp;
                count--;
                heap.MinHeapify(list, 0, count);
            }

            Array.Sort(array);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Array[{0}] = {1}, List[{0}] = {2}", i, array[i], list[i]);
            }
        }

        static void MinHeapSort()
        {
            var heap = new BinaryHeap<int>();
            var list = new List<int>() { 25, 1351, -52, -7861234, 364, 38 };

            int[] array = new int[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            heap.BuildMaxHeap(list);
            //Console.WriteLine(list[0]);

            int count = list.Count;

            for (int i = list.Count - 1; i > 1; i--)
            {
                var temp = list[i];
                list[i] = list[0];
                list[0] = temp;
                count--;
                heap.MaxHeapify(list, 0, count);
            }

            Array.Sort(array);

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Array[{0}] = {1}, List[{0}] = {2}", i, array[i], list[i]);
            }
        }

        public class ClassTest : IComparable<ClassTest>
        {

            public int Index { get; set; }

            public string Name { get; set; }

            public ClassTest(int index, string name)
            {
                Index = index;
                Name = name;
            }
            public int CompareTo(ClassTest other)
            {
                return this.Index - other.Index;
            }

            public override string ToString()
            {
                return string.Format("Index: {0}, Name: {1} ", Index, Name);
            }
        }

        static void Main(string[] args)
        {

            //MinHeapSort();
            //Console.WriteLine("---------------------------------");
            //MaxHeapSort();

            Console.WriteLine(string.Compare("1", "436q6"));

            //var heap = new BinaryHeap<ClassTest>();
            //var javaHeap = new BinaryJavaHeap<ClassTest>();

            //var list = new List<ClassTest>() { new ClassTest(1, "1"), new ClassTest(10, "new"),
            //new ClassTest(-3524, "reag"), new ClassTest(4579, "436q6"), new ClassTest(84756, "uhehg")};

            //ClassTest[] array = new ClassTest[list.Count];
            //list.CopyTo(array);

            //heap.BuildMinHeap(list);
            

            //for (int i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine(list[i]);
                
            //}

            //for (int i = 0; i < list.Count; i++)
            //{
            //    javaHeap.add(array[i]);

            //}

            //for (int i = 0; i < array.Length; i++)
            //{
            //    Console.WriteLine(javaHeap.h[i]);
            //}
        }
    }
}
