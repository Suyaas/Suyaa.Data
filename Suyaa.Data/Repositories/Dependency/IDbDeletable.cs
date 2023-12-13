using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 可更新数据库操作对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbDeletable<TEntity> where TEntity : IDbEntity
    {
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
    }
}
