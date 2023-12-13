using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Helpers
{
    /* 异步方法 */
    public static partial class RepositoryHelper
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        public static async Task UpdateAsync<TEntity, TId>(this IRepository<TEntity, TId> repository, TEntity entity)
            where TEntity : IDbEntity<TId>, new()
            where TId : notnull
        {
            await repository.UpdateAsync(entity, d => d.Id.Equals(entity.Id));
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        public static async Task DeleteAsync<TEntity, TId>(this IRepository<TEntity, TId> repository, TEntity entity)
            where TEntity : IDbEntity<TId>, new()
            where TId : notnull
        {
            await repository.DeleteAsync(d => d.Id.Equals(entity.Id));
        }
    }
}
