using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class DequeueNonPositiveQuantityException : BaseException
    {

        # region Constructors

        public DequeueNonPositiveQuantityException()
            : base()
        {
        }

        public DequeueNonPositiveQuantityException(string message)
            : base(message)
        {
        }

        public DequeueNonPositiveQuantityException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion

    }
}
