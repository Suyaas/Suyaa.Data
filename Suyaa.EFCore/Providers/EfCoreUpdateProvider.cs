using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.Data.Entities;
using Suyaa.Data.Factories;
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
    /// 数据更新操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public sealed class EfCoreUpdateProvider<TEntity> : IDbUpdateProvider<TEntity>
        where TEntity : class, IDbEntity
    {
        private readonly IEntityModelFactory _entityModelFactory;
        private readonly IDbWorkManager _dbWorkManager;
        private readonly DescriptorTypeDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbSetModel _entity;

        /// <summary>
        /// 数据更新操作供应商
        /// </summary>
        public EfCoreUpdateProvider(
            IEntityModelFactory entityModelFactory,
            IDbWorkManager dbWorkManager
            )
        {
            _entityModelFactory = entityModelFactory;
            _dbWorkManager = dbWorkManager;
            _dbContext = _dbWorkManager.GetCurrentWork().GetDbContext();
            _dbSet = _dbContext.Set<TEntity>();
            _entity = _entityModelFactory.GetDbSet<TEntity>(_dbContext);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var datas = _dbSet.Where(predicate).ToList();
            foreach (var field in _entity.Fields)
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
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        public void Update(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var datas = _dbSet.Where(predicate).ToList();
            var fields = _entity.GetEntityUpdateFields(selector);
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
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            var datas = await _dbSet.Where(predicate).ToListAsync();
            foreach (var field in _entity.Fields)
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
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity, Expression<Func<TEntity, object>> selector, Expression<Func<TEntity, bool>> predicate)
        {
            var datas = await _dbSet.Where(predicate).ToListAsync();
            var fields = _entity.GetEntityUpdateFields(selector);
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
