using Microsoft.EntityFrameworkCore;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Factories;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
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
    /// 数据更新操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class EfCoreUpdateProvider<TEntity> : IDbUpdateProvider<TEntity>
        where TEntity : class, IDbEntity
    {
        private readonly IEntityModelFactory _entityModelFactory;

        /// <summary>
        /// 数据更新操作供应商
        /// </summary>
        public EfCoreUpdateProvider(
            IEntityModelFactory entityModelFactory
            )
        {
            _entityModelFactory = entityModelFactory;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        public void Update(IDbWork work, TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            // 获取DbSet
            var dbSet = dbContext.Set<TEntity>();
            // 获取DbSet建模
            var dbSetModel = _entityModelFactory.GetDbSet<TEntity>(dbContext);
            var datas = dbSet.Where(predicate).ToList();
            foreach (var field in dbSetModel.Columns)
            {
                foreach (var data in datas)
                {
                    var value = field.PropertyInfo.GetValue(entity);
                    field.PropertyInfo.SetValue(data, value);
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        public void Update(IDbWork work, TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            // 获取DbSet
            var dbSet = dbContext.Set<TEntity>();
            // 获取DbSet建模
            var dbSetModel = _entityModelFactory.GetDbSet<TEntity>(dbContext);
            var datas = dbSet.Where(predicate).ToList();
            var fields = dbSetModel.GetEntityUpdateFields(selector);
            foreach (var field in fields)
            {
                foreach (var data in datas)
                {
                    var value = field.PropertyInfo.GetValue(entity);
                    field.PropertyInfo.SetValue(data, value);
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(IDbWork work, TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            // 获取DbSet
            var dbSet = dbContext.Set<TEntity>();
            // 获取DbSet建模
            var dbSetModel = _entityModelFactory.GetDbSet<TEntity>(dbContext);
            var datas = await dbSet.Where(predicate).ToListAsync();
            foreach (var field in dbSetModel.Columns)
            {
                foreach (var data in datas)
                {
                    var value = field.PropertyInfo.GetValue(entity);
                    field.PropertyInfo.SetValue(data, value);
                }
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(IDbWork work, TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            // 获取数据库上下文
            var dbContext = work.GetDbContext();
            // 获取DbSet
            var dbSet = dbContext.Set<TEntity>();
            // 获取DbSet建模
            var dbSetModel = _entityModelFactory.GetDbSet<TEntity>(dbContext);
            var datas = await dbSet.Where(predicate).ToListAsync();
            var fields = dbSetModel.GetEntityUpdateFields(selector);
            foreach (var field in fields)
            {
                foreach (var data in datas)
                {
                    var value = field.PropertyInfo.GetValue(entity);
                    field.PropertyInfo.SetValue(data, value);
                }
            }
        }
    }
}
