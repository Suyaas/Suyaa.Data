using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.Data.Sources;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Models;
using Suyaa.EFCore.Sources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 建模工厂助手
    /// </summary>
    public static class EntityModelFactoryHelper
    {
        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="factory"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static DbSetModel GetDbSet<TEntity>(this IEntityModelFactory factory, DescriptorTypeDbContext dbContext)
            where TEntity : IDbEntity
        {
            return (DbSetModel)factory.GetEntity(new TypeDbContextModelSource(typeof(TEntity), dbContext));
        }
    }
}
