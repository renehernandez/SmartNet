using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class InvalidItemComparisonHeapException : BaseException
    {
        # region Constructors

        public InvalidItemComparisonHeapException()
            : base()
        {
        }

        public InvalidItemComparisonHeapException(string message)
            : base(message)
        {
        }

        public InvalidItemComparisonHeapException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion
    }
}
