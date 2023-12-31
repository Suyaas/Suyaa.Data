﻿using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Descriptors.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Dependency
{
    /// <summary>
    /// 带描述的数据库上下文
    /// </summary>
    public interface IDefineDbContext
    {
        /// <summary>
        /// 数据库连接描述
        /// </summary>
        IDbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 数据库上下文配置
        /// </summary>
        DbContextOptions Options { get; }
    }
}
