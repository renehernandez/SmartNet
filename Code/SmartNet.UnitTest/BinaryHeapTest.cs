using NUnit.Framework;
using SmartNet.Interfaces;
using SmartNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.UnitTest
{
    [TestFixture]
    public class BinaryHeapTest
    {
        # region Private Fields

        IBinaryHeap<ClassTest> heap;

        List<ClassTest> list;

        ClassComparer classComparer;
        
        TestComparer testComparer;
        
        IntComparer intComparer;

        # endregion

        [SetUp]
        public void Init()
        {
            heap = new BinaryHeap<ClassTest>();
            classComparer = new ClassComparer();
            testComparer = new TestComparer();
            intComparer = new IntComparer();

            list = new List<ClassTest>() { new ClassTest(1, "1"), new ClassTest(10, "new"),
            new ClassTest(-3524, "reag"), new ClassTest(-4579, "436q6"), new ClassTest(84756, "uhehg")};
        }

        [Test]
        public void BuildMaxHeap()
        {
            heap.BuildMaxHeap(list);

            Assert.AreEqual(list[0].Index, 84756);
            Assert.True(list[0].CompareTo(list[1]) >= 0);
        }

        [Test]
        public void BuildMinHeap()
        {
            heap.BuildMinHeap(list);

            Assert.AreEqual(list[0].Index, -4579);
            Assert.True(list[0].CompareTo(list[1]) <= 0);
        }
        
        [Test]
        public void BuildMaxHeapComparer()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);
            heap.BuildMaxHeap(list);

            Assert.AreEqual(list[0].Name, "uhehg");
            Assert.True(testComparer.Compare(list[0], list[1]) >= 0);
        }

        [Test]
        public void BuildMinHeapComparer()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);

            heap.BuildMinHeap(list);

            Assert.AreEqual(list[0].Name, "1");
            Assert.True(testComparer.Compare(list[0], list[1]) <= 0);
        }

        [Test]
        public void MaxHeapSort()
        {
            ClassTest[] array = new ClassTest[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            heap.BuildMinHeap(list);
            
            int count = list.Count;

            for (int i = list.Count - 1; i > 0; i--)
            {
                var temp = list[i];
                list[i] = list[0];
                list[0] = temp;
                count--;
                heap.MinHeapify(list, 0, count);
            }

            Array.Sort(array, classComparer);

            CheckValues(array, list.ToArray());
        }

        [Test]
        public void MinHeapSort()
        {
            ClassTest[] array = new ClassTest[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            heap.BuildMaxHeap(list);

            int count = list.Count;

            for (int i = list.Count - 1; i > 0; i--)
            {
                var temp = list[i];
                list[i] = list[0];
                list[0] = temp;
                count--;
                heap.MaxHeapify(list, 0, count);
            }

            Array.Sort(array);

            CheckValues(array, list.ToArray());
        }

        [Test]
        public void MaxIntHeapSort()
        {
            var heap = new BinaryHeap<int>();
            var list = new List<int>() { 25, 1351, -52, -7861234, 364, 38 };

            int[] array = list.ToArray();

            heap.BuildMinHeap(list);

            int count = list.Count;

            for (int i = list.Count - 1; i > 0; i--)
            {
                var temp = list[i];
                list[i] = list[0];
                list[0] = temp;
                count--;
                heap.MinHeapify(list, 0, count);
            }

            Array.Sort(array, intComparer);

            CheckValues(array, list.ToArray());

        }

        [Test]
        public void MinIntHeapSort()
        {
            var heap = new BinaryHeap<int>();
            var list = new List<int>() { 25, 1351, -52, -7861234, 364, 38 };

            int[] array = list.ToArray();

            heap.BuildMaxHeap(list);

            int count = list.Count;

            for (int i = list.Count - 1; i > 0; i--)
            {
                var temp = list[i];
                list[i] = list[0];
                list[0] = temp;
                count--;
                heap.MaxHeapify(list, 0, count);
            }

            Array.Sort(array);

            CheckValues(array, list.ToArray());

        }

        # region Private Class

        private class IntComparer : IComparer<int>
        {

            public int Compare(int x, int y)
            {
                return y - x;
            }
        }

        private class ClassComparer : IComparer<ClassTest>
        {

            public int Compare(ClassTest x, ClassTest y)
            {
                return y.Index - x.Index;
            }
        }

        private class TestComparer : IComparer<ClassTest>
        {
            public int Compare(ClassTest x, ClassTest y)
            {
                return string.Compare(x.Name, y.Name);
            }
        }

        private class ClassTest : IComparable<ClassTest>
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
        }

        # endregion

        # region Private Methods

        private void CheckValues<T>(T[] array1, T[] array2)
        {
            Assert.AreEqual(array1.Length, array2.Length);

            for (int i = 0; i < array1.Length; i++)
            {
                Assert.AreEqual(array1[i], array2[i]);
            }
        }

        # endregion

    }
}
