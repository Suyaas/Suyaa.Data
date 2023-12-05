using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.PostgreSQL.Helpers
{
    /// <summary>
    /// 主机数据库上下文配置助手
    /// </summary>
    public static class DbConnectionDescriptorHelper
    {
        /// <summary>
        /// 获取Postgres
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DbContextOptions GetPostgreSqlContextOptions(this IDbConnectionDescriptor descriptor)
        {
            if (descriptor.DatabaseType != DatabaseType.PostgreSQL) throw new DbTypeNotSupportedException(descriptor.DatabaseType);
            // 添加数据库上下文配置
            var optionsBuilder = new DbContextOptionsBuilder<DescriptorDbContext>();
            optionsBuilder.UseNpgsql(descriptor.ToConnectionString());
            return optionsBuilder.Options;
        }
    }
}
