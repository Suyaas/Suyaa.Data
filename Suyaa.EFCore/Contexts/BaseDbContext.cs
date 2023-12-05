using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa.EFCore.Contexts
{
    /// <summary>
    /// EFCore重写上下文
    /// </summary>
    public abstract class BaseDbContext : DbContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        public BaseDbContext(DbContextOptions options, string connectionString) : base(options)
        {
            ConnectionString = connectionString;
        }

    }
}
