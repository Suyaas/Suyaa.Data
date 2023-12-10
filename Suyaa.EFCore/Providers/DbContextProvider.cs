using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 数据库上下文供应商
    /// </summary>
    public sealed class DbContextProvider : IDbContextProvider
    {
        private readonly List<IDescriptorDbContext> _dbContexts;

        /// <summary>
        /// 数据库上下文供应商
        /// </summary>
        public DbContextProvider()
        {
            _dbContexts = new List<IDescriptorDbContext>();
        }

        /// <summary>
        /// 获取数据库上下文集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDescriptorDbContext> GetDbContexts()
        {
            return _dbContexts;
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        public void AddDbContext(IDescriptorDbContext dbContext)
        {
            _dbContexts.Add(dbContext);
        }

        /// <summary>
        /// 清空数据库上下文
        /// </summary>
        public void ClearDbContexts()
        {
            _dbContexts?.Clear();
        }
    }
}
