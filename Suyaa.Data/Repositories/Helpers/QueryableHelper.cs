using Suyaa.Data.Entities;
using Suyaa.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Data.Repositories.Helpers
{
    /// <summary>
    /// 查询助手
    /// </summary>
    public static class QueryableHelper
    {
        /// <summary>
        /// 获取Sql语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string ToSql<TEntity>(this IQueryable<TEntity> query)
        {
            Type type = query.GetType();
            EntityQueryable<TEntity> entityQueryable = (EntityQueryable<TEntity>)query;
            return ((EntityQueryProvider)entityQueryable.Provider).GetSqlStatement(entityQueryable.Expression);
        }
    }
}
