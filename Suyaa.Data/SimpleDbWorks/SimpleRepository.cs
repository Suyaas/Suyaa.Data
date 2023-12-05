using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
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
            if (work is null) throw new NotExistException<IDbWork>();
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

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityDelete(_entity, predicate);
            // 生成参数
            //var parameters = _entity.GetParameters(entity);
            // 执行脚本
            GetSqlRepository().ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityDelete(_entity, predicate);
            // 生成参数
            //var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await GetSqlRepository().ExecuteNonQueryAsync(sql);
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

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.Fields, predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            GetSqlRepository().ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.GetEntityUpdateFields(selector), predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            GetSqlRepository().ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.Fields, predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await GetSqlRepository().ExecuteNonQueryAsync(sql, parameters);
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
            // 生成sql
            var sql = _dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.GetEntityUpdateFields(selector), predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await GetSqlRepository().ExecuteNonQueryAsync(sql, parameters);
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
