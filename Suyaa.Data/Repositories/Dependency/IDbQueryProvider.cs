using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Suyaa.Data.DbWorks.Dependency;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 数据库查询供应商
    /// </summary>
    public interface IDbQueryProvider<TEntity>
        where TEntity : IDbEntity
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query(IDbWork work);
    }
}
