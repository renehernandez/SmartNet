using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Models
{
    public static class Classic
    {

        # region Complete Graphs

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
            var graph = new TGraph {Data = {Name = string.Format("Complete ({0}) Graph", n)}};

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
            var graph = new TGraph { Data = { Name = string.Format("Complete ({0}) DiGraph", n) } };

            graph.AddEdges(Enumerable.Range(0, n).SelectMany(
                i => Enumerable.Range(0, n).Where(k => k != i).Select(j => new Tuple<int, int>(i, j))
                ));

            return graph;
        }

        # endregion

        # region Complete Bipartite Graph

        public static SGraph<int> CompleteBipartiteGraph(int n1, int n2)
        {
            return CompleteBipartiteGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>(n1, n2);
        }

        public static TGraph CompleteBipartiteGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n1, int n2)
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {

            var graph = new TGraph {Data = {Name = string.Format("Complete Bipartite ({0}, {1}) Graph", n1, n2)}};

            graph.AddVertices(Enumerable.Range(0, n1));
            graph.AddVertices(Enumerable.Range(n1, n2));

            graph.AddEdges(Enumerable.Range(0, n1).SelectMany(i => Enumerable.Range(n1, n2).Select(
                j => new Tuple<int, int>(i, j))));

            return graph;

        }

        # endregion

        # region Star Graph

        public static SGraph<int> StarGraph(int n)
        {
            return StarGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>(n);
        }

        public static TGraph StarGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n)
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = CompleteBipartiteGraph<TGraph, TEdge, TGraphData, TEdgeData>(1, n);
            graph.Data.Name = string.Format("Star ({0}) Graph", n);

            return graph;
        }

        # endregion

        # region Balanced Tree

        public static SGraph<int> BalancedTree(int b, int h)
        {
            return BalancedTree<SGraph<int>, SEdge<int>, GraphData, EdgeData>(b, h);
        }

        public static TGraph BalancedTree<TGraph, TEdge, TGraphData, TEdgeData>(int b, int h)
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = new TGraph();

            graph.AddVertex(0);

            int n = b;
            int current = 1;

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    graph.AddEdge(current, (current - 1)/b);
                    current += 1;
                }
                n *= b;
            }

            return graph;
        }

        # endregion

        # region Wheel Graph

        public static SGraph<int> WheelGraph(int n)
        {
            return WheelGraph<SGraph<int>, SEdge<int>, GraphData, EdgeData>(n);
        }

        public static TGraph WheelGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n)
            where TGraph : Graph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : Edge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = StarGraph<TGraph, TEdge, TGraphData, TEdgeData>(n);

            graph.AddCycle(Enumerable.Range(1, n));
            graph.Data.Name = string.Format("Wheel ({0}) Graph", n);

            return graph;
        }

        # endregion


    }
}
