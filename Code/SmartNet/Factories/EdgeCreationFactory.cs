using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SmartNet.Interfaces;

namespace SmartNet.Factories
{
    public static class EdgeCreationFactory
    {

        public static EdgeFromVerticesFactory<TVertex, TEdge, TData> GetEdgeFromVertices<TVertex, TEdge, TData>() 
            where TVertex : IEquatable<TVertex> 
            where TEdge : IEdge<TEdge, TVertex, TData> 
            where TData : IData, new()
        {
            var type = typeof(TEdge);

            var ctor = type.GetConstructor(new[] { typeof(TVertex), typeof(TVertex) });

            var length = ctor.GetParameters().Length;

            var source = Expression.Parameter(typeof(TVertex));

            var target = Expression.Parameter(typeof(TVertex));

            var argsExp = new Expression[length];

            argsExp[0] = Expression.Convert(source, typeof(TVertex));
            argsExp[1] = Expression.Convert(target, typeof(TVertex));

            NewExpression newExp = Expression.New(ctor, argsExp);
            var lambda = Expression.Lambda<EdgeFromVerticesFactory<TVertex, TEdge, TData>>(newExp, source, target);

            return lambda.Compile();
        }

        public static ReverseEdgeFactory<TVertex, TEdge, TData> GetReverseEdge<TVertex, TEdge, TData>() 
            where TVertex : IEquatable<TVertex> 
            where TEdge : IEdge<TEdge, TVertex, TData> 
            where TData : IData, new()
        {
            var edgeType = typeof (TEdge);
            var vertexType = typeof (TVertex);
            var dataType = typeof (TData);

            var ctor = edgeType.GetConstructor(new[] {vertexType, vertexType, dataType});

            var length = ctor.GetParameters().Length;

            var edge = Expression.Parameter(typeof (TEdge));

            var source = Expression.Variable(typeof(TVertex));
            var target = Expression.Variable(typeof(TVertex));
            var data = Expression.Variable(typeof (TData));

            var argsExp = new Expression[length];

            argsExp[0] = Expression.Convert(source, typeof(TVertex));
            argsExp[1] = Expression.Convert(target, typeof(TVertex));
            argsExp[2] = Expression.Convert(data, typeof(TData));

            var newExp = Expression.New(ctor, argsExp);
            var lambda = Expression.Lambda<ReverseEdgeFactory<TVertex, TEdge, TData>>(
                Expression.Block(
                    new []{ source, target, data },
                    Expression.Assign(target, Expression.Call(edge, edgeType.GetProperty("Source").GetGetMethod())),
                    Expression.Assign(source, Expression.Call(edge, edgeType.GetProperty("Target").GetGetMethod())),
                    Expression.Assign(data, Expression.Call(edge, edgeType.GetProperty("Data").GetGetMethod())),
                    newExp
                ), 
                edge
            );

            return lambda.Compile();
        } 

    }
}
