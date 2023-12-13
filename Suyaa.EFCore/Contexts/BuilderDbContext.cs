using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa.EFCore.Contexts
{
    /// <summary>
    /// 构建器数据库上下文
    /// </summary>
    public abstract class BuilderDbContext : BaseDbContext
    {

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="dbContextOptionsProvider"></param>
        /// <param name="dbContextOptionsBuilderProvider"></param>
        /// <param name="connectionString"></param>
        public BuilderDbContext(
            IDbContextOptionsProvider dbContextOptionsProvider,
            IDbContextOptionsBuilderProvider dbContextOptionsBuilderProvider,
            string connectionString)
            : base(dbContextOptionsProvider.GetDbContextOptions(dbContextOptionsBuilderProvider, connectionString))
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="dbContextOptionsProvider"></param>
        /// <param name="dbContextOptionsBuilderProvider"></param>
        /// <param name="connection"></param>
        public BuilderDbContext(
            IDbContextOptionsProvider dbContextOptionsProvider,
            IDbContextOptionsBuilderProvider dbContextOptionsBuilderProvider,
            DbConnection connection)
            : base(dbContextOptionsProvider.GetDbContextOptions(dbContextOptionsBuilderProvider, connection))
        {
            ConnectionString = connection.ConnectionString;
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; }
    }
}
