using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.Models;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace Suyaa.EFCore.DbWorks
{
    /// <summary>
    /// EfCore 作业
    /// </summary>
    public class EfCoreWork : Disposable, IDbWork
    {
        private readonly IDbContextFactory _dbContextFacotry;
        private readonly IEntityModelConventionFactory _entityConventionFactory;
        private readonly IDbWork _dbWork;
        private readonly DescriptorTypeDbContext _dbContext;
        private static Type _dbSetType = typeof(DbSet<>);

        /// <summary>
        /// EfCore 作业
        /// </summary>
        public EfCoreWork(
            IDbContextFactory dbContextFacotry,
            IEntityModelConventionFactory entityConventionFactory,
            IDbWork dbWork
            )
        {
            _dbContextFacotry = dbContextFacotry;
            _entityConventionFactory = entityConventionFactory;
            _dbWork = dbWork;
            var types = GetDbContextsDbSets(_dbContextFacotry.DbContexts);
            _dbContext = new DescriptorTypeDbContext(_entityConventionFactory, _dbWork, _dbWork.ConnectionDescriptor, _dbWork.Connection, types);
            _dbContext.Database.UseTransaction(_dbWork.Transaction);
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
        /// 作业管理器
        /// </summary>
        public IDbWorkManager WorkManager => _dbWork.WorkManager;

        /// <summary>
        /// 数据库连接
        /// </summary>
        public DbConnection Connection => _dbWork.Connection;

        /// <summary>
        /// 事务
        /// </summary>
        public DbTransaction Transaction => _dbWork.Transaction;

        /// <summary>
        /// 连接描述
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor => _dbWork.ConnectionDescriptor;

        /// <summary>
        /// 生效事务
        /// </summary>
        public void Commit()
        {
            _dbContext.SaveChanges();
            _dbWork.Commit();
        }

        /// <summary>
        /// 生效事务
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
            await _dbWork.CommitAsync();
        }

        /// <summary>
        /// 数据库命令创建
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public DbCommand? DbCommandCreating(DbCommand? dbCommand)
        {
            return _dbWork.DbCommandCreating(dbCommand);
        }

        /// <summary>
        /// 数据库命令执行
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DbCommand DbCommandExecuting(DbCommand dbCommand)
        {
            return _dbWork.DbCommandExecuting(dbCommand);
        }
    }
}
