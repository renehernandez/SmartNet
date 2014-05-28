using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class NullElementEnqueueException : BaseException
    {

        # region Constructors

        public NullElementEnqueueException()
            : base()
        {
        }

        public NullElementEnqueueException(string message)
            : base(message)
        {
        }

        public NullElementEnqueueException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion

    }
}
