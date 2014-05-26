using SmartNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet
{
    public class BaseContainer : IContainer
    {

        public int Weight { get; set; }

        public BaseContainer()
        {
            Weight = 1;
        }

    }
}
