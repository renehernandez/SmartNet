using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Models
{
    public static class Classic
    {

        public static TGraph EmptyGraph<TGraph, TEdge, TData>(int n)
            where TGraph : IGraph<TGraph, int, TEdge, TData>, new()
            where TEdge : IEdge<TEdge, int, TData>
            where TData : IData, new()
        {
            var graph = new TGraph();
            if (n > 0)
                graph.AddVertices(Enumerable.Range(0, n));
            return graph;
        }

    }
}
