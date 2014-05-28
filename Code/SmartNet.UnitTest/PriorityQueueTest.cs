using NUnit.Framework;
using SmartNet.Interfaces;
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

        public class TestCompare : IComparer<ClassTest>
        {
            public int Compare(ClassTest x, ClassTest y)
            {
                return String.Compare(x.Name, y.Name);
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
        }

        IPriorityQueue<ClassTest> queue;

    }
}
