using Suyaa.Data.Compilers;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Expressions;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Queries
{
    /// <summary>
    /// 实例查询供应商
    /// </summary>
    public class EntityQueryProvider : IQueryProvider
    {
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly IDbWork _work;
        private static readonly Type _enumerableType = typeof(IEnumerable<>);
        private static readonly Type _queryableType = typeof(Queryable);

        // 数据库查询供应商
        private readonly IDbScriptProvider _dbScriptProvider;
        private readonly ExpressionCompiler _compiler;

        /// <summary>
        /// 实例查询供应商
        /// </summary>
        public EntityQueryProvider(
            IEntityModelFactory entityModelFactory,
            IDbWork work
            )
        {
            _entityModelFactory = entityModelFactory;
            _work = work;
            var provider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            _dbScriptProvider = provider.ScriptProvider;
            _compiler = new ExpressionCompiler(_dbScriptProvider);
        }

        /// <summary>
        /// 创建查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        // 获取函数执行表达式
        private Expression GetMethodCallExpression(MethodCallExpression methodCallExpression, EntityModel model)
        {
            // Queryable函数
            if (methodCallExpression.Method.DeclaringType == _queryableType)
            {
                var methodCallName = methodCallExpression.Method.Name;
                switch (methodCallName)
                {
                    // Select
                    case nameof(Queryable.Select):
                        return new SelectExpression(methodCallExpression.Type, model, methodCallExpression.Arguments[0], methodCallExpression.Arguments[1]);
                    // Where
                    case nameof(Queryable.Where):
                        return new WhereExpression(methodCallExpression.Type, model, methodCallExpression.Arguments[0], methodCallExpression.Arguments[1]);
                }
            }
            return methodCallExpression;
        }

        /// <summary>
        /// 创建查询
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            var model = _entityModelFactory.GetEntity(typeof(TElement));
            var treatExpression = expression switch
            {
                MethodCallExpression methodCallExpression => GetMethodCallExpression(methodCallExpression, model),
                _ => expression,
            };
            return new EntityQueryable<TElement>(this, treatExpression);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取Sql语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string GetSqlStatement(Expression expression)
        {
            return _compiler.GetStatement(expression);
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        [return: MaybeNull]
        public TResult Execute<TResult>(Expression expression)
        {
            string sql = _compiler.GetStatement(expression);
            var sqlRepository = _work.GetSqlRepository();
            var type = typeof(TResult);
            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();
                // 返回列表
                if (genericTypeDefinition == _enumerableType)
                {
                    var genericType = type.GenericTypeArguments[0];
                    var model = _entityModelFactory.GetEntity(genericType);
                    var typeList = typeof(List<>);
                    var typeInstance = typeList.MakeGenericType(type.GenericTypeArguments);
                    var mothedAdd = typeInstance.GetMethod("Add");
                    var obj = Activator.CreateInstance(typeInstance);
                    if (obj is null) throw new TypeNotSupportedException(type);
                    sqlRepository.ExecuteReader(sql, reader =>
                    {
                        while (reader.ReadData(model, out object? data))
                        {
                            if (data is null) continue;
                            mothedAdd.Invoke(obj, new object[] { data });
                        }
                    });
                    return (TResult)obj;
                }
            }
            return sqlRepository.GetData<TResult>(sql);
        }
    }
}
