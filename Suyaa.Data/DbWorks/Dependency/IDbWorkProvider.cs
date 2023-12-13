using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Suyaa.Data.Enums;

namespace Suyaa.Data.DbWorks.Dependency
{
    /// <summary>
    /// 工作者供应商
    /// </summary>
    public interface IDbWorkProvider
    {
        /// <summary>
        /// 创建一个工作者
        /// </summary>
        /// <returns></returns>
        IDbWork CreateWork(IDbWorkManager dbWorkManager);
    }
}
