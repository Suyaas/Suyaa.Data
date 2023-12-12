using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
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
    public sealed class DbContextOptionsBuilderProvider : IDbContextOptionsBuilderProvider
    {
        /// <summary>
        /// 创建数据库上下文配置构建器
        /// </summary>
        /// <returns></returns>
        public DbContextOptionsBuilder<DbContext> CreateBuilder()
        {
            var builder = new DbContextOptionsBuilder<DbContext>();
            return builder;
        }
    }
}
