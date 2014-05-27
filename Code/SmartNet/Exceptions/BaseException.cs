using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public abstract class BaseException: Exception
    {

        # region Constructors

        protected BaseException()
            : base()
        {
        }

        protected BaseException(string message)
            : base(message)
        {
        }

        protected BaseException(string message, params object[] args)
            : this(string.Format(message, args))
        {
        }

        # endregion

    }
}
