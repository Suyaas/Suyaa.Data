using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Providers
{
    /// <summary>
    /// 数据新增操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class DbInsertProvider<TEntity> : IDbInsertProvider<TEntity>
        where TEntity : IDbEntity
    {
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly ISqlRepository _sqlRepository;
        private readonly DbEntityModel _entity;

        /// <summary>
        /// 数据新增操作供应商
        /// </summary>
        public DbInsertProvider(
            IEntityModelFactory entityModelFactory,
            ISqlRepository sqlRepository
            )
        {
            _entityModelFactory = entityModelFactory;
            _sqlRepository = sqlRepository;
            _entity = _entityModelFactory.GetDbEntity<TEntity>();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        public void Insert(IDbWork work, TEntity entity)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityInsert(_entity);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            _sqlRepository.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(IDbWork work, TEntity entity)
        {
            // 获取数据库供应商
            var dbProvider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            // 生成sql
            var sql = dbProvider.ScriptProvider.GetEntityInsert(_entity);
            // 生成参数
            var parameters = _entity.GetParameters(entity);
            // 执行脚本
            await _sqlRepository.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
