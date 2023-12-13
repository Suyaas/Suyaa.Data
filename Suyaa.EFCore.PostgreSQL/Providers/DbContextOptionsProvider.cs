using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.DbInterceptors;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.PostgreSQL.Providers
{
    /// <summary>
    /// 数据库上下文配置供应商
    /// </summary>
    public sealed class DbContextOptionsProvider : IDbContextOptionsProvider
    {
        /// <summary>
        /// 获取数据库上下文配置
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public DbContextOptions GetDbContextOptions(IDbContextOptionsBuilderProvider provider, string connectionString)
        {
            var optionsBuilder = provider.CreateBuilder();
            optionsBuilder.UseNpgsql(connectionString);
            //optionsBuilder.AddInterceptors(new EfCoreCommandInterceptor());
            return optionsBuilder.Options;
        }

        /// <summary>
        /// 获取数据库上下文配置
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public DbContextOptions GetDbContextOptions(IDbContextOptionsBuilderProvider provider, DbConnection connection)
        {
            var optionsBuilder = provider.CreateBuilder();
            optionsBuilder.UseNpgsql(connection);
            //optionsBuilder.AddInterceptors(new EfCoreCommandInterceptor());
            return optionsBuilder.Options;
        }
    }
}
