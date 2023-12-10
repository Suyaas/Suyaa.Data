﻿using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据库作业助手
    /// </summary>
    public static class DbWorkHelper
    {
        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        public static ISqlRepository GetSqlRepository(this IDbWork work, ISqlRepositoryProvider sqlRepositoryProvider)
        {
            return new SqlRepository(work.WorkManager, sqlRepositoryProvider);
        }
        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        public static IRepository<TEntity> GetRepository<TEntity>(
            this IDbWork work,
            IDbInsertProvider<TEntity> dbInsertProvider,
            IDbDeleteProvider<TEntity> dbDeleteProvider,
            IDbUpdateProvider<TEntity> dbUpdateProvider,
            IDbQueryProvider<TEntity> dbQueryProvider
            )
            where TEntity : IDbEntity, new()
        {
            return new Repository<TEntity>(dbInsertProvider, dbDeleteProvider, dbUpdateProvider, dbQueryProvider);
        }
        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        public static IRepository<TEntity, TId> GetRepository<TEntity, TId>(
            this IDbWork work,
            IDbInsertProvider<TEntity> dbInsertProvider,
            IDbDeleteProvider<TEntity> dbDeleteProvider,
            IDbUpdateProvider<TEntity> dbUpdateProvider,
            IDbQueryProvider<TEntity> dbQueryProvider
            )
            where TEntity : IDbEntity<TId>, new()
            where TId : notnull
        {
            return new Repository<TEntity, TId>(dbInsertProvider, dbDeleteProvider, dbUpdateProvider, dbQueryProvider);
        }
    }
}