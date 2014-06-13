using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet
{
    public class GraphData : IGraphData
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Graph Data: {0}Name: {1}{2}", "{", Name, "}");
        }
    }
}
