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
        private readonly List<IDefineDbContext> _dbContexts;

        /// <summary>
        /// 数据库上下文供应商
        /// </summary>
        public DbContextProvider()
        {
            _dbContexts = new List<IDefineDbContext>();
        }

        /// <summary>
        /// 获取数据库上下文集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDefineDbContext> GetDbContexts()
        {
            return _dbContexts;
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="dbContext"></param>
        public void AddDbContext(IDefineDbContext dbContext)
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
