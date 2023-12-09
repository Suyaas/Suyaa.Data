using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 可更新数据库操作对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbUpdatable<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步方式更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
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
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate);
    }
}
