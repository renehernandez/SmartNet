using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class DuplicatedEdgeException : BaseException
    {

        # region Constructors

        public DuplicatedEdgeException()
            : base()
        {
        }

        public DuplicatedEdgeException(string message)
            : base(message)
        {
        }

        public DuplicatedEdgeException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion

    }
}
