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

        public static SDiGraph<int> CompleteBipartiteDiGraph(int n1, int n2)
        {
            return CompleteBipartiteDiGraph<SDiGraph<int>, SDiEdge<int>, GraphData, EdgeData>(n1, n2);
        }

        public static TGraph CompleteBipartiteDiGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n1, int n2)
            where TGraph : DiGraph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : DiEdge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {

            var graph = new TGraph { Data = { Name = string.Format("Complete Bipartite ({0}, {1}) DiGraph", n1, n2) } };

            graph.AddVertices(Enumerable.Range(0, n1));
            graph.AddVertices(Enumerable.Range(n1, n2));

            graph.AddEdges(Enumerable.Range(0, n1).SelectMany(i => Enumerable.Range(n1, n2).Select(
                j => new Tuple<int, int>(i, j))));

            graph.AddEdges(Enumerable.Range(0, n1).SelectMany(i => Enumerable.Range(n1, n2).Select(
                j => new Tuple<int, int>(j, i))));

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

        public static SDiGraph<int> StarDiGraph(int n)
        {
            return StarDiGraph<SDiGraph<int>, SDiEdge<int>, GraphData, EdgeData>(n);
        }

        public static TGraph StarDiGraph<TGraph, TEdge, TGraphData, TEdgeData>(int n)
            where TGraph : DiGraph<int, TEdge, TGraphData, TEdgeData>, IGraph<TGraph, int, TEdge, TGraphData, TEdgeData>,
                new()
            where TEdge : DiEdge<int, TEdgeData>, IEdge<TEdge, int, TEdgeData>
            where TGraphData : IGraphData, new()
            where TEdgeData : IEdgeData, new()
        {
            var graph = CompleteBipartiteDiGraph<TGraph, TEdge, TGraphData, TEdgeData>(1, n);
            graph.Data.Name = string.Format("Star ({0}) DiGraph", n);

            return graph;
        }

        # endregion
    }
}
