using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Enums;
using Suyaa.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.SqlServer.Helpers
{
    /// <summary>
    /// 主机数据库上下文配置助手
    /// </summary>
    public static class DbConnectionDescriptorHelper
    {
        /// <summary>
        /// 获取Sqlite
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DbContextOptions GetSqliteContextOptions(this DbConnectionDescriptor descriptor)
        {
            if (descriptor.DatabaseType != DatabaseType.MicrosoftSqlServer) throw new DbTypeNotSupportedException(descriptor.DatabaseType);
            // 添加数据库上下文配置
            var optionsBuilder = new DbContextOptionsBuilder<DescriptorDbContext>();
            optionsBuilder.UseSqlite(descriptor.ToConnectionString());
            return optionsBuilder.Options;
        }
    }
}
