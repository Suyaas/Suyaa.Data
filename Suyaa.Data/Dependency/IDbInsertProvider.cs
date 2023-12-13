using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Data.DbWorks.Dependency;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据新增操作供应商
    /// </summary>
    public interface IDbInsertProvider<TEntity>
        where TEntity : IDbEntity
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        void Insert(IDbWork work, TEntity entity);
        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        Task InsertAsync(IDbWork work, TEntity entity);
    }
}
