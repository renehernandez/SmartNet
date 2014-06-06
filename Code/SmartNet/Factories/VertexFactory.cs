using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNet.Factories
{
    public delegate TVertex VertexFactory<out TVertex>(Dictionary<string, string> attributes);
}
