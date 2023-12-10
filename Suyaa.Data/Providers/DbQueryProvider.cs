﻿using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Providers
{
    /// <summary>
    /// 数据更新操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class DbQueryProvider<TEntity> : IDbQueryProvider<TEntity>
        where TEntity : IDbEntity
    {
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly IDbScriptProvider _dbScriptProvider;
        private readonly ISqlRepository _sqlRepository;
        private readonly DbEntityModel _entity;

        /// <summary>
        /// 数据更新操作供应商
        /// </summary>
        public DbQueryProvider(
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
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable<TEntity> Query()
        {
            throw new NotImplementedException();
        }
    }
}