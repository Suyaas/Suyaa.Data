using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Providers
{
    /// <summary>
    /// 数据删除操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class DbDeleteProvider<TEntity> : IDbDeleteProvider<TEntity>
        where TEntity : IDbEntity
    {
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly ISqlRepository _sqlRepository;
        private readonly DbEntityModel _entity;

        /// <summary>
        /// 数据删除操作供应商
        /// </summary>
        public DbDeleteProvider(
            IEntityModelFactory entityModelFactory,
            ISqlRepository sqlRepository
            )
        {
            _entityModelFactory = entityModelFactory;
            _sqlRepository = sqlRepository;
            _entity = _entityModelFactory.GetDbEntity<TEntity>();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="predicate"></param>
        public void Delete(IDbWork work, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityDelete(_entity, predicate);
            // 生成参数
            //var parameters = _entity.GetParameters(entity);
            // 执行脚本
            _sqlRepository.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(IDbWork work, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityDelete(_entity, predicate);
            // 生成参数
            //var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await _sqlRepository.ExecuteNonQueryAsync(sql);
        }
    }
}
