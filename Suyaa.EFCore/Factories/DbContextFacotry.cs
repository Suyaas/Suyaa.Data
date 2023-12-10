using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Factories
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class DbContextFacotry : IDbContextFacotry
    {
        private readonly IEnumerable<IDbContextProvider> _providers;
        private readonly List<IDescriptorDbContext> _dbContexts;

        /// <summary>
        /// 数据库上下文工厂
        /// </summary>
        /// <param name="providers"></param>
        public DbContextFacotry(
            IEnumerable<IDbContextProvider> providers
            )
        {
            _providers = providers;
            _dbContexts = new List<IDescriptorDbContext>();
            foreach (var provider in _providers)
            {
                _dbContexts.AddRange(provider.GetDbContexts());
            }
        }

        /// <summary>
        /// 获取数据库上下文集合
        /// </summary>
        public IEnumerable<IDescriptorDbContext> DbContexts => _dbContexts;
    }
}
