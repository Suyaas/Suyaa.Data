using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文配置构建器供应商
    /// </summary>
    public interface IDbContextOptionsBuilderProvider
    {
        /// <summary>
        /// 创建数据库上下文配置构建器
        /// </summary>
        /// <returns></returns>
        DbContextOptionsBuilder<DbContext> CreateBuilder();
    }
}
