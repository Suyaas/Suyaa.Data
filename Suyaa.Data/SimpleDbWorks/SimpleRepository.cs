using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.SimpleDbWorks
{
    /// <summary>
    /// 简单的数据仓库
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SimpleRepository<TEntity> : IRepository<TEntity>
        where TEntity : IEntity, new()
    {
        private readonly EntityDescriptor _entity;
        private readonly IDbProvider _dbProvider;
        private readonly IDbWorkManager _dbWorkManager;

        /// <summary>
        /// 简单的数据仓库
        /// </summary>
        public SimpleRepository(
            IDbWorkManager dbWorkManager
            )
        {
            _entity = dbWorkManager.Factory.GetEntity<TEntity>();
            _dbProvider = dbWorkManager.Factory.Provider;
            _dbWorkManager = dbWorkManager;
        }

        /// <summary>
        /// 获取数据库工作者
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public IDbWork GetDbWork()
        {
            var work = _dbWorkManager.GetCurrentWork();
            if (work is null) throw new DbException("Repository db work not found.");
            return work;
        }

        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        public ISqlRepository GetSqlRepository()
        {
            return GetDbWork().GetSqlRepository();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityInsert(_entity);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            GetSqlRepository().ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityInsert(_entity);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await GetSqlRepository().ExecuteNonQueryAsync(sql, parameters);
        }

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> Query()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// 简单的数据仓库
    /// </summary>
    public class SimpleRepository<TEntity, TId> : SimpleRepository<TEntity>, IRepository<TEntity, TId>
        where TEntity : IEntity<TId>, new()
        where TId : notnull
    {
        /// <summary>
        /// 简单的数据仓库
        /// </summary>
        /// <param name="dbWorkManager"></param>
        public SimpleRepository(IDbWorkManager dbWorkManager) : base(dbWorkManager)
        {
        }
    }
}
