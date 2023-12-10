using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库删除操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbDeleteProvider<TEntity>
        where TEntity : IDbEntity
    {
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="predicate"></param>
        void Delete(IDbWork work, Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 异步方式删除数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="predicate"></param>
        Task DeleteAsync(IDbWork work, Expression<Func<TEntity, bool>> predicate);
    }
}
