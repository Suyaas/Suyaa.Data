using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 工作者供应商
    /// </summary>
    public interface IDbWorkProvider
    {
        /// <summary>
        /// 获取数据库供应商
        /// </summary>
        IDbProvider GetDbProvider(DatabaseType databaseType);

        /// <summary>
        /// 获取当前工作者
        /// </summary>
        /// <returns></returns>
        IDbWork? GetCurrentWork();

        /// <summary>
        /// 设置当前工作者
        /// </summary>
        /// <returns></returns>
        void SetCurrentWork(IDbWork work);

        /// <summary>
        /// 释放工作者
        /// </summary>
        void ReleaseWork();

        /// <summary>
        /// 创建一个工作者
        /// </summary>
        /// <returns></returns>
        IDbWork CreateWork(DbConnectionDescriptor dbConnectionDescriptor);
    }
}
