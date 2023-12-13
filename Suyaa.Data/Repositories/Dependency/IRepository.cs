using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    public interface IRepository<TEntity> : IDbInsertable<TEntity>, IDbDeletable<TEntity>, IDbUpdatable<TEntity>, IDbQueryable<TEntity>
        where TEntity : IDbEntity, new()
    {
    }
    /// <summary>
    /// 带主键的数据仓库
    /// </summary>
    public interface IRepository<TEntity, TId> : IRepository<TEntity>
        where TEntity : IDbEntity<TId>, new()
        where TId : notnull
    {
    }
}
