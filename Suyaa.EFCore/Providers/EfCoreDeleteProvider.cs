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
        private readonly IDbWorkManager _dbWorkManager;
        private readonly DescriptorTypeDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// 数据删除操作供应商
        /// </summary>
        public EfCoreDeleteProvider(
            IDbWorkManager dbWorkManager
            )
        {
            _dbWorkManager = dbWorkManager;
            _dbContext = _dbWorkManager.GetCurrentWork().GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var datas = _dbSet.Where(predicate).AsNoTracking().ToList();
            _dbContext.Remove(datas);
        }

        /// <summary>
        /// 异步删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var datas = await _dbSet.Where(predicate).AsNoTracking().ToListAsync();
            _dbContext.Remove(datas);
        }
    }
}
