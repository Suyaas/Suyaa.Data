using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.DbWorks.Dependency
{
    /// <summary>
    /// 数据库作业拦截器工厂
    /// </summary>
    public interface IDbWorkInterceptorFactory
    {
        /// <summary>
        /// 获取拦截器集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDbWorkInterceptor> GetInterceptors();
    }
}
