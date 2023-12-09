using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Suyaa.Data
{
    /// <summary>
    /// 数据仓库
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity, new()
    {
        private readonly DbEntityModel _entity;
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly IDbWorkManager _dbWorkManager;
        private readonly IDbInsertProvider<TEntity> _dbInsertProvider;
        private readonly IDbDeleteProvider<TEntity> _dbDeleteProvider;
        private readonly IDbUpdateProvider<TEntity> _dbUpdateProvider;
        private readonly IDbQueryProvider<TEntity> _dbQueryProvider;

        /// <summary>
        /// 数据仓库
        /// </summary>
        public Repository(
            IEntityModelFactory entityModelFactory,
            IDbWorkManager dbWorkManager,
            IDbInsertProvider<TEntity> dbInsertProvider,
            IDbDeleteProvider<TEntity> dbDeleteProvider,
            IDbUpdateProvider<TEntity> dbUpdateProvider,
            IDbQueryProvider<TEntity> dbQueryProvider
            )
        {
            _entityModelFactory = entityModelFactory;
            _dbWorkManager = dbWorkManager;
            _dbInsertProvider = dbInsertProvider;
            _dbDeleteProvider = dbDeleteProvider;
            _dbUpdateProvider = dbUpdateProvider;
            _dbQueryProvider = dbQueryProvider;
            _entity = _entityModelFactory.GetDbEntity<TEntity>();
        }

        /// <summary>
        /// 实体建模
        /// </summary>
        public DbEntityModel Entity => _entity;

        /// <summary>
        /// 获取数据库工作者
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public IDbWork GetDbWork()
        {
            var work = _dbWorkManager.GetCurrentWork();
            if (work is null) throw new NotExistException<IDbWork>();
            return work;
        }

        #region 新增

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            _dbInsertProvider.Insert(entity);
        }

        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            await _dbInsertProvider.InsertAsync(entity);
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _dbDeleteProvider.Delete(predicate);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            await _dbDeleteProvider.DeleteAsync(predicate);
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
            _dbUpdateProvider.Update(entity, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            _dbUpdateProvider.Update(entity, selector, predicate);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            await _dbUpdateProvider.UpdateAsync(entity, predicate);
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
            await _dbUpdateProvider.UpdateAsync(entity, selector, predicate);
        }

        #endregion

        #region 查询

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            return _dbQueryProvider.Query();
        }

        #endregion
    }
    /// <summary>
    /// 数据仓库
    /// </summary>
    public class Repository<TEntity, TId> : Repository<TEntity>, IRepository<TEntity, TId>
        where TEntity : IEntity<TId>, new()
        where TId : notnull
    {
        /// <summary>
        /// 数据仓库
        /// </summary>
        public Repository(
            IEntityModelFactory entityModelFactory,
            IDbWorkManager dbWorkManager,
            IDbInsertProvider<TEntity> dbInsertProvider,
            IDbDeleteProvider<TEntity> dbDeleteProvider,
            IDbUpdateProvider<TEntity> dbUpdateProvider,
            IDbQueryProvider<TEntity> dbQueryProvider
            ) : base(entityModelFactory, dbWorkManager, dbInsertProvider, dbDeleteProvider, dbUpdateProvider, dbQueryProvider)
        {
        }
    }
}
