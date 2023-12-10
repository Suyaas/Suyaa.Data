using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 数据新增操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class EfCoreInsertProvider<TEntity> : IDbInsertProvider<TEntity>
        where TEntity : class, IDbEntity
    {
        private readonly IDbWorkManager _dbWorkManager;
        private readonly DescriptorTypeDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// 数据新增操作供应商
        /// </summary>
        public EfCoreInsertProvider(
            IDbWorkManager dbWorkManager
            )
        {
            _dbWorkManager = dbWorkManager;
            _dbContext = _dbWorkManager.GetCurrentWork().GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            _dbContext.Add(entity);
        }

        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntity entity)
        {
            _dbContext.Add(entity);
            await Task.CompletedTask;
        }
    }
}
