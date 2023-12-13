using System;
using System.Collections.Generic;
using System.Text;
using Suyaa.Data.Dependency;

namespace Suyaa.Data.DbWorks.Dependency
{
    /// <summary>
    /// 数据库工作者管理器
    /// </summary>
    public interface IDbWorkManager
    {
        /// <summary>
        /// 连接描述
        /// </summary>
        IDbConnectionDescriptor ConnectionDescriptor { get; }
        /// <summary>
        /// 创建一个新的作业
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork();
        /// <summary>
        /// 获取当前作业
        /// </summary>
        /// <returns></returns>
        IDbWork? GetCurrentWork();
        /// <summary>
        /// 设置当前作业
        /// </summary>
        /// <returns></returns>
        void SetCurrentWork(IDbWork work);
        /// <summary>
        /// 释放当前作业
        /// </summary>
        void ReleaseWork();
    }
}
