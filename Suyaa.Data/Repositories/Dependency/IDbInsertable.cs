using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 可新增数据库操作对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbInsertable<TEntity> where TEntity : IDbEntity
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
    }
}
