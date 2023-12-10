using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Models;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;

namespace Suyaa.Data
{
    /// <summary>
    /// 简单的数据库工作着
    /// </summary>
    public sealed class EFCoreWork : Disposable, IDbWork
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbContextFacotry _dbContextFacotry;
        private readonly IDbWorkManager _dbWorkManager;
        private DbConnection? _connection;
        private DbTransaction? _transaction;
        private readonly DescriptorTypeDbContext _dbContext;
        private static Type _dbSetType = typeof(DbSet<>);

        /// <summary>
        /// 简单的数据库工作着
        /// </summary>
        public EFCoreWork(
            IDbFactory dbFactory,
            IDbContextFacotry dbContextFacotry,
            IDbWorkManager dbWorkManager,
            IDbContextOptionsProvider dbContextOptionsProvider
            )
        {
            ConnectionDescriptor = dbWorkManager.ConnectionDescriptor;
            _dbFactory = dbFactory;
            _dbContextFacotry = dbContextFacotry;
            _dbWorkManager = dbWorkManager;
            var types = GetDbContextsDbSets(_dbContextFacotry.DbContexts);
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
                if (!type.IsBased(_dbSetType)) continue;
                types.Add(prop.PropertyType);
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
