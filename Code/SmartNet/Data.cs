using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class Data: IData
    {
        public double Weight { get; set; }

        public override string ToString()
        {
            return string.Format("{0}Weight: {1}{2}", "{", Weight, "}");
        }
    }
}
