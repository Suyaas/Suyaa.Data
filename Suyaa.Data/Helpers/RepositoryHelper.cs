using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 仓库助手
    /// </summary>
    public static partial class RepositoryHelper
    {
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        public static void Update<TEntity, TId>(this IRepository<TEntity, TId> repository, TEntity entity)
            where TEntity : IEntity<TId>, new()
            where TId : notnull
        {
            repository.Update(entity, d => d.Id.Equals(entity.Id));
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        public static void Delete<TEntity, TId>(this IRepository<TEntity, TId> repository, TEntity entity)
            where TEntity : IEntity<TId>, new()
            where TId : notnull
        {
            repository.Delete(d => d.Id.Equals(entity.Id));
        }
    }
}
