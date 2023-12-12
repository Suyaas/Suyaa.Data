using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.Models;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace Suyaa.Data
{
    /// <summary>
    /// EfCore 作业
    /// </summary>
    public class EfCoreWork : DbWork
    {
        private readonly IDbContextFactory _dbContextFacotry;
        private readonly IEntityModelConventionFactory _entityConventionFactory;
        private readonly DescriptorTypeDbContext _dbContext;
        private static Type _dbSetType = typeof(DbSet<>);

        /// <summary>
        /// EfCore 作业
        /// </summary>
        public EfCoreWork(
            IDbWorkManager dbWorkManager,
            IDbFactory dbFactory,
            IDbContextFactory dbContextFacotry,
            IEntityModelConventionFactory entityConventionFactory
            ) : base(dbWorkManager, dbFactory)
        {
            _dbContextFacotry = dbContextFacotry;
            _entityConventionFactory = entityConventionFactory;
            var types = GetDbContextsDbSets(_dbContextFacotry.DbContexts);
            _dbContext = new DescriptorTypeDbContext(_entityConventionFactory, this, ConnectionDescriptor, types);
        }

        // 添加数据库实例
        private List<Type> GetDbContextDbSets(IDefineDbContext dbContext)
        {
            List<Type> types = new List<Type>();
            var type = dbContext.GetType();
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                // 跳过非泛型
                if (!prop.PropertyType.IsGenericType) continue;
                if (!prop.PropertyType.IsBased(_dbSetType)) continue;
                var entityType = prop.PropertyType.GenericTypeArguments[0];
                types.Add(entityType);
            }
            return types;
        }

        // 添加数据库上下文
        private List<Type> GetDbContextsDbSets(IEnumerable<IDefineDbContext> dbContexts)
        {
            List<Type> types = new List<Type>();
            foreach (var dbContext in dbContexts)
            {
                types.AddRange(GetDbContextDbSets(dbContext));
            }
            return types;
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        public DescriptorTypeDbContext DbContext => _dbContext;

        /// <summary>
        /// 生效事务
        /// </summary>
        public override void Commit()
        {
            _dbContext.SaveChanges();
            base.Commit();
        }

        /// <summary>
        /// 生效事务
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public override async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
            await base.CommitAsync();
        }
    }
}
