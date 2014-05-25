using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
{
    public class VertexNotFoundException : BaseException
    {

        # region Constructors

        public VertexNotFoundException()
            : base()
        {

        }

        public VertexNotFoundException(string message)
            : base(message)
        {

        }

        # endregion

    }
}
