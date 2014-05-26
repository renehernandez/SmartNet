﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Exceptions
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

        public EdgeNotFoundException(string message, params object[] args)
            : base(message, args)
        {
        }

        # endregion

    }
}
