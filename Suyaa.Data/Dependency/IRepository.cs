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
    public interface IRepository<TEntity>
        where TEntity : IEntity, new()
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="entity"></param>
        Task InsertAsync(TEntity entity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步方式删除数据
        /// </summary>
        /// <param name="predicate"></param>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        void Update(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步方式更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步方式更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Query();
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
