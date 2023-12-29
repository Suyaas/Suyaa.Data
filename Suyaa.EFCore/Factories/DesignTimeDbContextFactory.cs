using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Descriptors.Dependency;

namespace Suyaa.EFCore.Factories
{
    /// <summary>
    /// 设计时数据库上下文工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DesignTimeDbContextFactory<T> : IDesignTimeDbContextFactory<T>
        where T : DbContext
    {
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;

        /// <summary>
        /// 设计时数据库上下文工厂
        /// </summary>
        /// <param name="dbConnectionDescriptorFactory"></param>
        public DesignTimeDbContextFactory(
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory
            )
        {
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
        }

        /// <summary>
        /// 创建DbContext实例
        /// </summary>
        /// <param name="dbConnectionDescriptorFactory"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract T CreateDbContext(IDbConnectionDescriptorFactory dbConnectionDescriptorFactory, string[] args);

        /// <summary>
        /// 创建DbContext实例
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public T CreateDbContext(string[] args)
        {
            return this.CreateDbContext(_dbConnectionDescriptorFactory, args);
        }
    }
}
