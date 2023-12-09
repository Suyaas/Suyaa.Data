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
    /// 数据库上下文配置供应商
    /// </summary>
    public interface IDbContextOptionsProvider
    {
        /// <summary>
        /// 获取数据库上下文配置
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        DbContextOptions GetDbContextOptions(string connectionString);
    }
}
