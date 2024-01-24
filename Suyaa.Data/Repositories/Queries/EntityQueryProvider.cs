using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Helpers;
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
        private readonly IDbWork _work;
        private static readonly Type _enumerableType = typeof(IEnumerable<>);

        // 数据库查询供应商
        private readonly IDbScriptProvider _dbScriptProvider;

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

        /// <summary>
        /// 创建查询
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new EntityQueryable<TElement>(this, expression);
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
        /// 执行查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        [return: MaybeNull]
        public TResult Execute<TResult>(Expression expression)
        {
            string sql = _dbScriptProvider.GetStatement(expression);
            var sqlRepository = _work.GetSqlRepository();
            var type = typeof(TResult);
            if (type.IsInterface)
            {
                // 返回列表
                if (type == _enumerableType)
                {
                    var genericType = type.GenericTypeArguments[0];
                    var typeList = typeof(List<>);
                    var typeInstance = typeList.MakeGenericType(type.GenericTypeArguments);
                    var mothedAdd = typeInstance.GetMethod("Add");
                    var obj = Activator.CreateInstance(typeInstance);
                    if (obj is null) throw new TypeNotSupportedException(type);
                    sqlRepository.ExecuteReader(sql, reader =>
                    {
                        var data = reader.GetData(genericType);
                        if (data is null) return;
                        mothedAdd.Invoke(obj, new object[] { data });
                    });
                    return (TResult)obj;
                }
            }
            return sqlRepository.GetData<TResult>(sql);
        }

        /// <summary>
        /// 实例查询供应商
        /// </summary>
        /// <param name="work"></param>
        public EntityQueryProvider(IDbWork work)
        {
            _work = work;
            var provider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            _dbScriptProvider = provider.ScriptProvider;
        }
    }
}
