using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    public interface IRepository<TEntity> : IDbInsertable<TEntity>, IDbDeletable<TEntity>, IDbUpdatable<TEntity>, IDbQueryable<TEntity>
        where TEntity : IEntity, new()
    {

        /// <summary>
        /// 实例对象描述
        /// </summary>
        DbEntityModel Entity { get; }

    }
    /// <summary>
    /// 带主键的数据仓库
    /// </summary>
    public interface IRepository<TEntity, TId> : IRepository<TEntity>
        where TEntity : IEntity<TId>, new()
        where TId : notnull
    {
    }
}
