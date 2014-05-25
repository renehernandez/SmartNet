using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphNet.Core.Exceptions
{
    public class EdgeNotFoundException : BaseException
    {

        # region Constructors

        public EdgeNotFoundException()
            : base()
        {

        }

        public EdgeNotFoundException(string message)
            : base(message)
        {

        }

        # endregion

    }
}
