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
    /// 简单的数据库工作着
    /// </summary>
    public sealed class EfCoreWork : Disposable, IDbWork
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbContextFactory _dbContextFacotry;
        private readonly IDbWorkManager _dbWorkManager;
        private DbConnection? _connection;
        private DbTransaction? _transaction;
        private readonly DescriptorTypeDbContext _dbContext;
        private static Type _dbSetType = typeof(DbSet<>);

        /// <summary>
        /// 简单的数据库工作着
        /// </summary>
        public EfCoreWork(
            IDbFactory dbFactory,
            IDbContextFactory dbContextFacotry,
            IDbWorkManager dbWorkManager
            )
        {
            ConnectionDescriptor = dbWorkManager.ConnectionDescriptor;
            _dbFactory = dbFactory;
            _dbContextFacotry = dbContextFacotry;
            _dbWorkManager = dbWorkManager;
            var types = GetDbContextsDbSets(_dbContextFacotry.DbContexts);
            var efCoreProvider = ConnectionDescriptor.DatabaseType.GetEfCoreProvider();
            var dbContextOptionsProvider = efCoreProvider.DbContextOptionsProvider;
            _dbContext = new DescriptorTypeDbContext(ConnectionDescriptor, dbContextOptionsProvider.GetDbContextOptions(ConnectionDescriptor.ToConnectionString()), types);
        }

        // 添加数据库实例
        private List<Type> GetDbContextDbSets(IDescriptorDbContext dbContext)
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
        private List<Type> GetDbContextsDbSets(IEnumerable<IDescriptorDbContext> dbContexts)
        {
            List<Type> types = new List<Type>();
            foreach (var dbContext in dbContexts)
            {
                types.AddRange(GetDbContextDbSets(dbContext));
            }
            return types;
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        public DescriptorTypeDbContext DbContext => _dbContext;

        /// <summary>
        /// 数据库连接
        /// </summary>
        public DbConnection Connection => _connection ??= _dbFactory.GetDbConnection(ConnectionDescriptor);

        /// <summary>
        /// 事务
        /// </summary>
        public DbTransaction Transaction => _transaction ??= Connection.BeginTransaction();

        /// <summary>
        /// 工作者管理器
        /// </summary>
        public IDbWorkManager WorkManager => _dbWorkManager;

        /// <summary>
        /// 生效事务
        /// </summary>
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// 生效事务
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnManagedDispose()
        {
            base.OnManagedDispose();
            // 释放工作者
            _dbWorkManager.ReleaseWork();
            // 断开连接
            if (!(_connection is null))
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
