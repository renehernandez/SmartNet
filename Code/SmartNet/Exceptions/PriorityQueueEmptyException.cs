using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class PriorityQueueEmptyException : BaseException
    {

        # region Constructors

        public PriorityQueueEmptyException()
            : base()
        {
        }

        public PriorityQueueEmptyException(string message)
            : base(message)
        {
        }

        public PriorityQueueEmptyException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion

    }
}
