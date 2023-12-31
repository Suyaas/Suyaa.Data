﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Suyaa.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore
{
    /// <summary>
    /// 数据库生成器
    /// </summary>
    public interface IDbCreater
    {
        /// <summary>
        /// 数据库确保创建
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task<bool> EnsureCreated(BaseDbContext context);

        /// <summary>
        /// 获取数据库确保创建语句
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string GetEnsureCreatedSql(BaseDbContext context);

        /// <summary>
        /// 获取数据表创建语句
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        string GetCreateTableSql(IEntityType table);

        /// <summary>
        /// 获取数据列描述语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        string GetColumnSql(IEntityType table, IProperty column);

        /// <summary>
        /// 获取数据列创建语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        string GetAddColumnSql(IEntityType table, IProperty column);

        /// <summary>
        /// 获取数据列修改语句
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        string GetAlterColumnSql(IEntityType table, IProperty column);

    }
}
