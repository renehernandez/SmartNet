using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class DuplicatedVertexException : BaseException
    {

        # region Constructors

        public DuplicatedVertexException()
            : base()
        {
        }

        public DuplicatedVertexException(string message)
            : base(message)
        {
        }

        public DuplicatedVertexException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion

    }
}
