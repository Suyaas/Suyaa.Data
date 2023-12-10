using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库供应商
    /// </summary>
    public interface IEfCoreProvider
    {
        /// <summary>
        /// 数据库上下文配置供应商
        /// </summary>
        IDbContextOptionsProvider DbContextOptionsProvider { get; }
    }
}
