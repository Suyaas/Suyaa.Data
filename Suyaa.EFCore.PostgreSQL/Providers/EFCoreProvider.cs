using Microsoft.EntityFrameworkCore.Infrastructure;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.PostgreSQL.Providers
{
    /// <summary>
    /// EFCore 供应商
    /// </summary>
    public sealed class EfCoreProvider : IEfCoreProvider
    {
        private readonly IDbContextOptionsProvider _dbContextOptionsProvider;

        /// <summary>
        /// EFCore 供应商
        /// </summary>
        public EfCoreProvider()
        {
            _dbContextOptionsProvider = new DbContextOptionsProvider();
        }

        /// <summary>
        /// 数据库上下文配置供应商
        /// </summary>
        public IDbContextOptionsProvider DbContextOptionsProvider => _dbContextOptionsProvider;
    }
}
