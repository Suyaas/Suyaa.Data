using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Suyaa.Data.Repositories
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IDbEntity, new()
    {
        private readonly IDbWorkManager _dbWorkManager;
        private readonly IDbInsertProvider<TEntity> _dbInsertProvider;
        private readonly IDbDeleteProvider<TEntity> _dbDeleteProvider;
        private readonly IDbUpdateProvider<TEntity> _dbUpdateProvider;
        private readonly IDbQueryProvider<TEntity> _dbQueryProvider;

        /// <summary>
        /// 数据仓库
        /// </summary>
        public Repository(
            IDbWorkManager dbWorkManager,
            IDbInsertProvider<TEntity> dbInsertProvider,
            IDbDeleteProvider<TEntity> dbDeleteProvider,
            IDbUpdateProvider<TEntity> dbUpdateProvider,
            IDbQueryProvider<TEntity> dbQueryProvider
            )
        {
            _dbWorkManager = dbWorkManager;
            _dbInsertProvider = dbInsertProvider;
            _dbDeleteProvider = dbDeleteProvider;
            _dbUpdateProvider = dbUpdateProvider;
            _dbQueryProvider = dbQueryProvider;
        }

        // 获取当前作业
        private IDbWork GetDbWork()
        {
            var work = _dbWorkManager.GetCurrentWork();
            if (work is null) throw new NullException<IDbWork>();
            return work;
        }

        #region 新增

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            // 获取当前作业
            var work = GetDbWork();
            _dbInsertProvider.Insert(work, entity);
        }

        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            // 获取当前作业
            var work = GetDbWork();
            await _dbInsertProvider.InsertAsync(work, entity);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            _dbDeleteProvider.Delete(work, predicate);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            await _dbDeleteProvider.DeleteAsync(work, predicate);
        }

        #endregion

        #region 修改

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            _dbUpdateProvider.Update(work, entity, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            _dbUpdateProvider.Update(work, entity, selector, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            await _dbUpdateProvider.UpdateAsync(work, entity, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取当前作业
            var work = GetDbWork();
            await _dbUpdateProvider.UpdateAsync(work, entity, selector, predicate);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            // 获取当前作业
            var work = GetDbWork();
            return _dbQueryProvider.Query(work);
        }

        #endregion
    }
    /// <summary>
    /// 数据仓库
    /// </summary>
    public class Repository<TEntity, TId> : Repository<TEntity>, IRepository<TEntity, TId>
        where TEntity : IDbEntity<TId>, new()
        where TId : notnull
    {
        /// <summary>
        /// 数据仓库
        /// </summary>
        public Repository(
            IDbWorkManager dbWorkManager,
            IDbInsertProvider<TEntity> dbInsertProvider,
            IDbDeleteProvider<TEntity> dbDeleteProvider,
            IDbUpdateProvider<TEntity> dbUpdateProvider,
            IDbQueryProvider<TEntity> dbQueryProvider
            ) : base(dbWorkManager, dbInsertProvider, dbDeleteProvider, dbUpdateProvider, dbQueryProvider)
        {
        }
    }
}
