using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.SqlServer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.SqlServer
{
    /// <summary>
    /// SqlServer数据库上下文
    /// </summary>
    public abstract class SqlServerContext : DescriptorDbContext
    {
        /// <summary>
        /// SqlServer数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        protected SqlServerContext(DbConnectionDescriptor descriptor) : base(descriptor, descriptor.GetSqlServerContextOptions())
        {
        }
    }
}
