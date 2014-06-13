using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet
{
    public class EdgeData: IEdgeData
    {
        public double Weight { get; set; }

        public override string ToString()
        {
            return string.Format("Edge Data: {0}Weight: {1}{2}", "{", Weight, "}");
        }
    }
}
