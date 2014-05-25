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

        public BaseException()
            : base()
        {

        }

        public BaseException(string message)
            : base(message)
        {

        }

        # endregion

    }
}
