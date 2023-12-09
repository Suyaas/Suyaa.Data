using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 工作者管理器供应商
    /// </summary>
    public interface IDbWorkManagerProvider
    {
        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <returns></returns>
        IDbWorkManager CreateManager();
    }
}
