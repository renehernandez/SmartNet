using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class QuantityNotStoredQueueException: BaseException
    {

        # region Constructors

        public QuantityNotStoredQueueException()
            : base()
        {
        }

        public QuantityNotStoredQueueException(string message)
            : base(message)
        {
        }

        public QuantityNotStoredQueueException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion

    }
}
