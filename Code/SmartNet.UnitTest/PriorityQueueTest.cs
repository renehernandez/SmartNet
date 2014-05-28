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
    public class PriorityQueueTest
    {
        # region Private Fields

        IPriorityQueue<ClassTest> queue;

        List<ClassTest> classData;

        # endregion

        [SetUp]
        public void Init()
        {
            queue = new PriorityQueue<ClassTest>();

            classData = new List<ClassTest>(){new ClassTest(78356, "auhsd"), new ClassTest(9845, "iaueri34"),
            new ClassTest(-3534, "1653haafv"), new ClassTest(-3524, "-98asd"), new ClassTest(0, "aauhd")};
        }

        # region Constructors Tests

        [Test]
        public void StateQueueEmptyConstructor()
        {
            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateQueueComparerConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer());

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateQueueCapacityConstructor()
        {
            queue = new PriorityQueue<ClassTest>(1000);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateQueueBinaryHeapConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new BinaryHeap<ClassTest>());

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateQueueComparerCapacityConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer(), 1000);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateQueueBinaryHeapCapacityConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new BinaryHeap<ClassTest>(new TestComparer()), 4456);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateQueueEnumarebleConstructor()
        {
            queue = new PriorityQueue<ClassTest>(classData);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, classData.Count);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateQueueParamsCollectionConstructor()
        {
            queue = new PriorityQueue<ClassTest>(classData[0], classData[2], classData[3]);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 3);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateQueueComparerEnumerableConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer(), classData);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, classData.Count);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateQueueComparerParamsArrayConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer(), classData[1], classData[2], classData[0], classData[1]);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 4);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateQueueBinaryHeapEnumerableConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new BinaryHeap<ClassTest>(), classData);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, classData.Count);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateQueueBinaryHeapParamsArrayConstructor()
        {
            queue = new PriorityQueue<ClassTest>(new BinaryHeap<ClassTest>(), classData[4], classData[1]);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 2);
            Assert.False(queue.IsEmpty());
        }

        # endregion

        # region Clear Tests

        [Test]
        public void ClearEmptyQueue()
        {
            queue.Clear();

            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void ClearNonEmptyQueue()
        {
            queue.Enqueue(classData);

            queue.Clear();

            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        # endregion

        # region Enqueue Tests

        [Test]
        public void QueueIsNotEmptyAfterEnquee()
        {
            queue.Enqueue(classData[0]);

            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void CountIncrementAfterEnqueue()
        {
            queue.Enqueue(classData[1]);

            Assert.AreEqual(queue.Count, 1);
        }

        [Test]
        public void CountEnumerableEnqueue()
        {
            queue.Enqueue(classData);

            Assert.AreEqual(queue.Count, classData.Count);
        }

        [Test]
        public void CountParamsArrayEnqueue()
        {
            queue.Enqueue(classData[2], classData[1], classData[0]);

            Assert.AreEqual(queue.Count, 3);
        }

        [Test]
        [ExpectedException(typeof(NullElementEnqueueException))]
        public void EnqueueNullElement()
        {
            ClassTest klass = null;
            queue.Enqueue(klass);
        }

        [Test]
        public void MaximumValueAfterEnqueue()
        {
            queue.Enqueue(classData[3], classData[1], classData[4]);

            Assert.AreEqual(queue.Peek().Index, classData[1].Index);
        }

        # endregion

        # region Dequeue Tests

        [Test]
        [ExpectedException(typeof(PriorityQueueEmptyException))]
        public void DequeueEmptyQueue()
        {
            queue.Dequeue();
        }

        [Test]
        [ExpectedException(typeof(QuantityNotStoredQueueException))]
        public void DequeueQuantityNotAvailable()
        {
            queue.Enqueue(classData[0]);

            queue.Dequeue(10);
        }

        [Test]
        [ExpectedException(typeof(DequeueNonPositiveQuantityException))]
        public void DequeueNonPositiveQuantity()
        {
            queue.Enqueue(classData);

            queue.Dequeue(-20);
        }

        [Test]
        public void DequeueMaximumValue()
        {
            queue.Enqueue(classData[1], classData[2], classData[3], classData[0]);

            Assert.AreEqual(queue.Dequeue().Index, classData[0].Index);
        }

        # endregion

        # region Peek Tests

        [Test]
        public void CountIsTheSameAfterPeek()
        {
            queue.Enqueue(classData[4]);

            var item = queue.Peek();

            Assert.AreEqual(queue.Count, 1);
        }

        [Test]
        [ExpectedException(typeof(PriorityQueueEmptyException))]
        public void PeekEmptyQueue()
        {
            queue.Peek();
        }

        [Test]
        public void PeekIsGreater()
        {
            queue.Enqueue(classData);
            var top = queue.Peek();

            Assert.AreEqual(top.Index, 78356);
        }

        [Test]
        public void PeekMaximumValueAfterDequeue()
        {
            queue.Enqueue(classData);
            queue.Dequeue();

            Assert.AreEqual(queue.Peek().Index, classData[1].Index);
        }

        # endregion

        # region Private Class

        private class TestComparer : IComparer<ClassTest>
        {
            public int Compare(ClassTest x, ClassTest y)
            {
                return String.Compare(x.Name, y.Name);
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
