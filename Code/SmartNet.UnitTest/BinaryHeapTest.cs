using NUnit.Framework;
using SmartNet.Exceptions;
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

        List<ClassTest> classData;

        MaxClassComparer maxClassComparer;
        
        TestComparer testComparer;
        
        IntComparer intComparer;

        # endregion

        [SetUp]
        public void Init()
        {
            heap = new BinaryHeap<ClassTest>();
            maxClassComparer = new MaxClassComparer();
            testComparer = new TestComparer();
            intComparer = new IntComparer();

            classData = new List<ClassTest>() { new ClassTest(1, "1"), new ClassTest(10, "new"),
            new ClassTest(-3524, "reag"), new ClassTest(-4579, "436q6"), new ClassTest(84756, "uhehg")};
        }

        # region Constructors Tests

        [Test]
        public void StateEmptyConstructor()
        {
            Assert.IsNotNull(heap);
        }

        [Test]
        public void StateComparerConstructor()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);

            Assert.IsNotNull(heap);
        }

        # endregion

        # region HeapIncreaseKey Tests

        [Test]
        public void HeapIncreaseKey()
        {
            heap.BuildMaxHeap(classData);
            heap.HeapIncreaseKey(classData, 4, new ClassTest(11, "24"));

            Assert.AreEqual(11, classData[1].Index);
            Assert.AreEqual(10, classData[4].Index);
        }

        [Test]
        [ExpectedException(typeof(InvalidItemComparisonHeapException))]
        public void HeapIncreaseKeyException()
        {
            heap.BuildMaxHeap(classData);

            heap.HeapIncreaseKey(classData, 4, new ClassTest(-78453542, "hello"));
        }

        [Test]
        public void HeapIncreaseKeyComparer()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);
            heap.BuildMaxHeap(classData);
            heap.HeapIncreaseKey(classData, 3, new ClassTest(-848342, "zaoidvaibv"));

            Assert.AreEqual("new", classData[3].Name);
            Assert.AreEqual("uhehg", classData[1].Name);
            Assert.AreEqual("zaoidvaibv", classData[0].Name);
        }

        [Test]
        [ExpectedException(typeof(InvalidItemComparisonHeapException))]
        public void HeapIncreaseKeyComparerException()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);
            heap.BuildMaxHeap(classData);

            heap.HeapIncreaseKey(classData, 2, new ClassTest(-848342, "raag"));
        }

        # endregion

        # region HeapDecreaseKey Tests

        [Test]
        public void HeapDecreaseKey()
        {
            heap.BuildMinHeap(classData);
            heap.HeapDecreaseKey(classData, 3, new ClassTest(-4579, "oaiubfoi"));

            Assert.AreEqual("436q6", classData[0].Name);
            Assert.AreEqual("oaiubfoi", classData[1].Name);
            Assert.AreEqual(1, classData[3].Index);
        }

        [Test]
        [ExpectedException(typeof(InvalidItemComparisonHeapException))]
        public void HeapDecreaseKeyException()
        {
            heap.BuildMinHeap(classData);
            heap.HeapDecreaseKey(classData, 3, new ClassTest(9845, "oaiubfoi"));
        }

        [Test]
        public void HeapDecreaseKeyComparer()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);
            heap.BuildMinHeap(classData);
            heap.HeapDecreaseKey(classData, 4, new ClassTest(98245, "008349ADBA"));

            Assert.AreEqual("008349ADBA", classData[0].Name);
            Assert.AreEqual("1", classData[1].Name);
            Assert.AreEqual("436q6", classData[4].Name);
        }

        [Test]
        [ExpectedException(typeof(InvalidItemComparisonHeapException))]
        public void HeapDecreaseKeyComparerException()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);
            heap.BuildMinHeap(classData);

            heap.HeapDecreaseKey(classData, 1, new ClassTest(-98245, "baola;bdv"));
        }

        # endregion

        # region BuildHeap Tests

        [Test]
        public void BuildMaxHeap()
        {
            heap.BuildMaxHeap(classData);

            Assert.AreEqual(84756, classData[0].Index);
            Assert.True(classData[0].CompareTo(classData[1]) >= 0);
        }

        [Test]
        public void BuildMinHeap()
        {
            heap.BuildMinHeap(classData);

            Assert.AreEqual(-4579, classData[0].Index);
            Assert.True(classData[0].CompareTo(classData[1]) <= 0);
        }
        
        [Test]
        public void BuildMaxHeapComparer()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);
            heap.BuildMaxHeap(classData);

            Assert.AreEqual("uhehg", classData[0].Name);
            Assert.True(testComparer.Compare(classData[0], classData[1]) >= 0);
        }

        [Test]
        public void BuildMinHeapComparer()
        {
            heap = new BinaryHeap<ClassTest>(testComparer);

            heap.BuildMinHeap(classData);

            Assert.AreEqual("1", classData[0].Name);
            Assert.True(testComparer.Compare(classData[0], classData[1]) <= 0);
        }

        # endregion

        # region Sort Tests

        [Test]
        public void MaxHeapSort()
        {
            ClassTest[] array = classData.ToArray();

            heap.BuildMinHeap(classData);
            
            int count = classData.Count;

            for (int i = classData.Count - 1; i > 0; i--)
            {
                var temp = classData[i];
                classData[i] = classData[0];
                classData[0] = temp;
                count--;
                heap.MinHeapify(classData, 0, count);
            }

            Array.Sort(array, maxClassComparer);

            CheckValues(array, classData.ToArray());
        }

        [Test]
        public void MinHeapSort()
        {
            ClassTest[] array = classData.ToArray();

            heap.BuildMaxHeap(classData);

            int count = classData.Count;

            for (int i = classData.Count - 1; i > 0; i--)
            {
                var temp = classData[i];
                classData[i] = classData[0];
                classData[0] = temp;
                count--;
                heap.MaxHeapify(classData, 0, count);
            }

            Array.Sort(array);

            CheckValues(array, classData.ToArray());
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

        # endregion

        # region Private Class

        private class IntComparer : IComparer<int>
        {

            public int Compare(int x, int y)
            {
                return y - x;
            }
        }

        private class MaxClassComparer : IComparer<ClassTest>
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
