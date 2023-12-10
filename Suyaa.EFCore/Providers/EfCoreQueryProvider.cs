using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 数据更新操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class EfCoreQueryProvider<TEntity> : IDbQueryProvider<TEntity>
        where TEntity : class, IDbEntity
    {
        /// <summary>
        /// 获取查询
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IQueryable<TEntity> Query(IDbWork work)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            return dbContext.Set<TEntity>();
        }
    }
}
