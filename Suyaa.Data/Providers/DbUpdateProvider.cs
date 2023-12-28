using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Providers
{
    /// <summary>
    /// 数据更新操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class DbUpdateProvider<TEntity> : IDbUpdateProvider<TEntity>
        where TEntity : IDbEntity
    {
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly ISqlRepository _sqlRepository;
        private readonly DbEntityModel _entity;

        /// <summary>
        /// 数据更新操作供应商
        /// </summary>
        public DbUpdateProvider(
            IEntityModelFactory entityModelFactory,
            ISqlRepository sqlRepository
            )
        {
            _entityModelFactory = entityModelFactory;
            _sqlRepository = sqlRepository;
            _entity = _entityModelFactory.GetDbEntity<TEntity>();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        public void Update(IDbWork work, TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.Columns, predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            _sqlRepository.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        public void Update(IDbWork work, TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.GetEntityUpdateFields(selector), predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            _sqlRepository.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(IDbWork work, TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.Columns, predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await _sqlRepository.ExecuteNonQueryAsync(sql, parameters);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(IDbWork work, TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityUpdate(_entity, _entity.GetEntityUpdateFields(selector), predicate);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await _sqlRepository.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
