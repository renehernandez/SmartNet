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
        public void StateInQueueEmptyConstruction()
        {
            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateInQueueComparerConstruction()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer());

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateInQueueCapacityConstruction()
        {
            queue = new PriorityQueue<ClassTest>(1000);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateInQueueComparerCapacityConstruction()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer(), 1000);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 0);
            Assert.True(queue.IsEmpty());
        }

        [Test]
        public void StateInQueueEnumarebleConstruction()
        {
            queue = new PriorityQueue<ClassTest>(classData);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, classData.Count);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateInQueueParamsCollectionConstruction()
        {
            queue = new PriorityQueue<ClassTest>(classData[0], classData[2], classData[3]);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 3);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateInQueueComparerEnumerableConstruction()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer(), classData);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, classData.Count);
            Assert.False(queue.IsEmpty());
        }

        [Test]
        public void StateInQueueComparerParamsCollectionConstruction()
        {
            queue = new PriorityQueue<ClassTest>(new TestComparer(), classData[1], classData[2], classData[0], classData[1]);

            Assert.IsNotNull(queue);
            Assert.AreEqual(queue.Count, 4);
            Assert.False(queue.IsEmpty());
        }

        # endregion

        # region Enqueue Test

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

        # endregion

        # region Peek Test

        [Test]
        public void CountIsTheSameAfterPeek()
        {
            queue.Enqueue(classData[4]);

            var item = queue.Peek();

            Assert.AreEqual(queue.Count, 1);
        }

        [Test]
        [ExpectedException(typeof(PriorityQueueEmptyException))]
        public void QueueIsEmptyWhenPeek()
        {
            queue.Peek();
        }

        # endregion

        # region Dequeue Test

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
