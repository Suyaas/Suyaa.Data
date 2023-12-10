using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库更新操作供应商
    /// </summary>
    public interface IDbUpdateProvider<TEntity>
        where TEntity : IDbEntity
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        void Update(IDbWork work, TEntity entity, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步方式更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        Task UpdateAsync(IDbWork work, TEntity entity, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        void Update(IDbWork work, TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步方式更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        Task UpdateAsync(IDbWork work, TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate);
    }
}
