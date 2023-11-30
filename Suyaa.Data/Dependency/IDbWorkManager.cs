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
        /// 创建一个新的工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork();
    }
}
