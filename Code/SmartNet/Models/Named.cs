using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Models
{
    public class Named
    {

        public static SGraph<int> PetersenGraph()
        {
            return PetersenGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>();
        }

        public static TGraph PetersenGraph<TGraph, TEdge, TGraphData, TEdgeData>()
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>, new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = new TGraph { Data = { Name = "Petersen Graph" } };

            graph.AddCycle(Enumerable.Range(0, 5));
            graph.AddCycle(5, 7, 9, 6, 8);

            graph.AddEdges(Enumerable.Range(0, 5).Select(i => new Tuple<int, int>(i, i + 5)));

            return graph;
        }

        public static SDiGraph<int> PetersenDiGraph()
        {
            return PetersenDiGraph<SDiGraph<int>, SDiEdge<int>, GraphData, EdgeData>();
        }

        public static TGraph PetersenDiGraph<TGraph, TEdge, TGraphData, TEdgeData>()
            where TGraph : DiGraph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>, new()
            where TEdge : DiEdge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = new TGraph { Data = { Name = "Petersen DiGraph" } };

            graph.AddCycle(Enumerable.Range(0, 5));
            graph.AddCycle(4, 3, 2, 1, 0);

            graph.AddCycle(5, 7, 9, 6, 8);
            graph.AddCycle(8, 6, 9, 7, 5);

            graph.AddEdges(Enumerable.Range(0, 5).Select(i => new Tuple<int, int>(i, i + 5)));
            graph.AddEdges(Enumerable.Range(0, 5).Select(i => new Tuple<int, int>(i + 5, i)));

            return graph;
        }

    }
}
