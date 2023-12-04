using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库工作者管理器
    /// </summary>
    public interface IDbWorkManager
    {
        /// <summary>
        /// 数据库工厂
        /// </summary>
        IDbFactory Factory { get; }
        /// <summary>
        /// 连接描述
        /// </summary>
        IDbConnectionDescriptor ConnectionDescriptor { get; }
        /// <summary>
        /// 创建一个新的工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork();
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
    }
}
