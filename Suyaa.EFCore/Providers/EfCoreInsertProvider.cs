using Microsoft.EntityFrameworkCore;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
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

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        public void Insert(IDbWork work, TEntity entity)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            dbContext.Add(entity);
        }

        /// <summary>
        /// 异步方式新增数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(IDbWork work, TEntity entity)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            dbContext.Add(entity);
            await Task.CompletedTask;
        }
    }
}
