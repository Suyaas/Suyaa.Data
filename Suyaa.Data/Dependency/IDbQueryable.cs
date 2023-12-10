using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 可查询数据库操作对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbQueryable<TEntity> where TEntity : IDbEntity
    {
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();
    }
}
