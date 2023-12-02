using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.EFCore.PostgreSQL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.PostgreSQL
{
    /// <summary>
    /// SqlServer数据库上下文
    /// </summary>
    public abstract class PostgreSqlContext : DbDescriptorContext
    {
        /// <summary>
        /// SqlServer数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        protected PostgreSqlContext(DbConnectionDescriptor descriptor) : base(descriptor, descriptor.GetPostgreSqlContextOptions())
        {
        }
    }
}
