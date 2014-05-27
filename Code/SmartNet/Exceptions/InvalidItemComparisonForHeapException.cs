using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class InvalidItemComparisonForHeapException : BaseException
    {
        # region Constructors

        public InvalidItemComparisonForHeapException()
            : base()
        {
        }

        public InvalidItemComparisonForHeapException(string message)
            : base(message)
        {
        }

        public InvalidItemComparisonForHeapException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion
    }
}
