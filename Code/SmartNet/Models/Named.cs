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

        # region House Graph

        public static SGraph<int> HouseGraph()
        {
            return HouseGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>();
        }

        public static TGraph HouseGraph<TGraph, TEdge, TGraphData, TEdgeData>()
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {

            var graph = new TGraph();

            graph.AddCycle(Enumerable.Range(0, 5));
            graph.AddEdge(1, 4);
            graph.Data.Name = "House Graph";

            return graph;
        }

        public static SGraph<int> HouseXGraph()
        {
            return HouseGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>();
        }

        public static TGraph HouseXGraph<TGraph, TEdge, TGraphData, TEdgeData>()
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {

            var graph = new TGraph();

            graph.AddCycle(Enumerable.Range(0, 5));
            graph.AddEdge(1, 4);
            graph.AddEdge(1, 3);
            graph.AddEdge(4, 2);
            graph.Data.Name = "House X Graph";

            return graph;
        }

        # endregion

        # region Petersen Graph

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

        # endregion

    }
}
