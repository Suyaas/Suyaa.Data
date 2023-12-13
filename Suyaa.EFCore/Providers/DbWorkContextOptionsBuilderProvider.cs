using Microsoft.EntityFrameworkCore;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.EFCore.DbInterceptors;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 数据库上下文配置构建器供应商
    /// </summary>
    public sealed class DbWorkContextOptionsBuilderProvider : IDbContextOptionsBuilderProvider
    {
        private readonly IDbWork _dbWork;

        /// <summary>
        /// 数据库上下文配置构建器供应商
        /// </summary>
        /// <param name="dbWork"></param>
        public DbWorkContextOptionsBuilderProvider(IDbWork dbWork)
        {
            _dbWork = dbWork;
        }

        /// <summary>
        /// 创建数据库上下文配置构建器
        /// </summary>
        /// <returns></returns>
        public DbContextOptionsBuilder<DbContext> CreateBuilder()
        {
            var builder = new DbContextOptionsBuilder<DbContext>();
            builder.AddInterceptors(new DbWorkCommandInterceptor(_dbWork), new DbWorkTransactionInterceptor());
            return builder;
        }
    }
}
