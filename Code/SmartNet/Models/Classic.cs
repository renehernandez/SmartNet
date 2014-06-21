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

        public static SGraph<int> CompleteGraph(int n)
        {
            return CompleteGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>(n);
        }

        public static TGraph CompleteGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n)
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>,IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>, new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData,new() 
            where TEdgeData : IEdgeData, new()
        {
            var graph = new TGraph {Data = {Name = string.Format("K_{0} Graph", n)}};

            graph.AddEdges(Enumerable.Range(0, n).SelectMany(
                i => Enumerable.Range(i + 1, n - i - 1).Select(j => new Tuple<int, int>(i, j))
                ));

            return graph;
        }

        public static SDiGraph<int> CompleteDiGraph(int n)
        {
            return CompleteDiGraph<SDiGraph<int>, SDiEdge<int>, GraphData, EdgeData>(n);
        }

        public static TGraph CompleteDiGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n)
            where TGraph : DiGraph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>, new()
            where TEdge : DiEdge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = new TGraph { Data = { Name = string.Format("K_{0} DiGraph", n) } };

            graph.AddEdges(Enumerable.Range(0, n).SelectMany(
                i => Enumerable.Range(0, n).Where(k => k != i).Select(j => new Tuple<int, int>(i, j))
                ));

            return graph;
        }


        public static SGraph<int> PetersenGraph(int n)
        {
            return PetersenGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>(n);
        }

        public static TGraph PetersenGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n)
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>,IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>, new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData,new() 
            where TEdgeData : IEdgeData, new()
        {
            var graph = new TGraph { Data = { Name = "Petersen Graph"} };

            graph.AddCycle(Enumerable.Range(0, 5));
            graph.AddCycle(Enumerable.Range(5, 5));

            graph.AddEdges(Enumerable.Range(0, 5).Select(i => new Tuple<int, int>(i, i + 5)));

            return graph;
        }

        public static SDiGraph<int> PetersenDiGraph(int n)
        {
            return PetersenDiGraph<SDiGraph<int>, SDiEdge<int>, GraphData, EdgeData>(n);
        }

        public static TGraph PetersenDiGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n)
            where TGraph : DiGraph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>, new()
            where TEdge : DiEdge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = new TGraph { Data = { Name = "Petersen DiGraph" } };

            graph.AddCycle(Enumerable.Range(0, 5));
            graph.AddCycle(4, 3, 2, 1, 0);

            graph.AddCycle(Enumerable.Range(5, 5));
            graph.AddCycle(9, 8, 7, 6, 5);

            graph.AddEdges(Enumerable.Range(0, 5).Select(i => new Tuple<int, int>(i, i + 5)));
            graph.AddEdges(Enumerable.Range(0, 5).Select(i => new Tuple<int, int>(i + 5, i)));

            return graph;
        }

    }
}
