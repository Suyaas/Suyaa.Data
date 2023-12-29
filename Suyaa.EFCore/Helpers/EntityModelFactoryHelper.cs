using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Models;
using Suyaa.EFCore.Sources;

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
