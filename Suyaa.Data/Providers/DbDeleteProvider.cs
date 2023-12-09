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
        where TEntity : IEntity
    {
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly IDbScriptProvider _dbScriptProvider;
        private readonly ISqlRepository _sqlRepository;
        private readonly DbEntityModel _entity;

        /// <summary>
        /// 数据删除操作供应商
        /// </summary>
        public DbDeleteProvider(
            IEntityModelFactory entityModelFactory,
            IDbScriptProvider dbScriptProvider,
            ISqlRepository sqlRepository
            )
        {
            _entityModelFactory = entityModelFactory;
            _dbScriptProvider = dbScriptProvider;
            _sqlRepository = sqlRepository;
            _entity = _entityModelFactory.GetDbEntity<TEntity>();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            // 生成sql
            var sql = _dbScriptProvider.GetEntityDelete(_entity, predicate);
            // 生成参数
            //var parameters = _entity.GetParameters(entity);
            // 执行脚本
            _sqlRepository.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            // 生成sql
            var sql = _dbScriptProvider.GetEntityDelete(_entity, predicate);
            // 生成参数
            //var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await _sqlRepository.ExecuteNonQueryAsync(sql);
        }
    }
}
