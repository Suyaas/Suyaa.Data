using Microsoft.EntityFrameworkCore;
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
    /// 数据库上下文配置供应商
    /// </summary>
    public sealed class DbContextOptionsProvider : IDbContextOptionsProvider
    {
        /// <summary>
        /// 获取数据库上下文配置
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DbContextOptions GetDbContextOptions(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            return optionsBuilder.Options;
        }
    }
}
