using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa.EFCore.Contexts
{
    /// <summary>
    /// 带描述的数据库上下文
    /// </summary>
    public abstract class DescriptorDbContext : BaseDbContext, IDescriptorDbContext
    {

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="options"></param>
        public DescriptorDbContext(IDbConnectionDescriptor descriptor, DbContextOptions options) : base(descriptor.DatabaseType.GetEfCoreProvider().DbContextOptionsProvider.GetDbContextOptions(descriptor.ToConnectionString()))
        {
            ConnectionDescriptor = descriptor;
            Options = options;
        }

        /// <summary>
        /// 数据库连接描述
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 数据库上下文配置
        /// </summary>
        public DbContextOptions Options { get; }
    }
}
