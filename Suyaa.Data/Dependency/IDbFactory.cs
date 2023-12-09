using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库实例工厂
    /// </summary>
    public interface IDbFactory
    {
        ///// <summary>
        ///// 实例描述集合
        ///// </summary>
        //IList<DbEntityModel> Entities { get; }
        ///// <summary>
        ///// 获取实例描述
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //DbEntityModel GetEntity(Type type);
        ///// <summary>
        ///// 数据库工作者供应商
        ///// </summary>
        //IDbWorkProvider WorkProvider { get; }
        ///// <summary>
        ///// 工作者管理器供应商
        ///// </summary>
        //IDbWorkManagerProvider WorkManagerProvider { get; }
        ///// <summary>
        ///// 数据库供应商
        ///// </summary>
        //IDbProvider Provider { get; }
        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <returns></returns>
        DbConnection GetDbConnection(IDbConnectionDescriptor dbConnectionDescriptor);
        /// <summary>
        /// 获取新的参数索引号
        /// </summary>
        /// <returns></returns>
        long GetParamterIndex();
    }
}
