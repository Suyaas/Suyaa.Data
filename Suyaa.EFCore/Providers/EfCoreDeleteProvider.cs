using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 数据删除操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class EfCoreDeleteProvider<TEntity> : IDbDeleteProvider<TEntity>
        where TEntity : class, IDbEntity
    {

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="predicate"></param>
        public void Delete(IDbWork work, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            // 获取DbSet
            var dbSet = dbContext.Set<TEntity>();
            var datas = dbSet.Where(predicate).AsNoTracking().ToList();
            dbContext.Remove(datas);
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(IDbWork work, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            // 获取DbSet
            var dbSet = dbContext.Set<TEntity>();
            var datas = await dbSet.Where(predicate).AsNoTracking().ToListAsync();
            dbContext.Remove(datas);
        }
    }
}
