using Suyaa.Data.DbWorks.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 数据库作业拦截器工厂
    /// </summary>
    public class DbWorkInterceptorFactory : IDbWorkInterceptorFactory
    {
        private readonly List<IDbWorkInterceptor> _interceptors;

        /// <summary>
        /// 数据库作业拦截器工厂
        /// </summary>
        public DbWorkInterceptorFactory()
        {
            _interceptors = new List<IDbWorkInterceptor>();
        }

        /// <summary>
        /// 数据库作业拦截器工厂
        /// </summary>
        /// <param name="interceptors"></param>
        public DbWorkInterceptorFactory(IEnumerable<IDbWorkInterceptor> interceptors)
        {
            _interceptors = new List<IDbWorkInterceptor>();
            _interceptors.AddRange(interceptors);
        }

        /// <summary>
        /// 获取拦截器集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDbWorkInterceptor> GetInterceptors()
        {
            return _interceptors;
        }

        /// <summary>
        /// 添加拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void Add(IDbWorkInterceptor interceptor)
        {
            if (_interceptors.Contains(interceptor)) return;
            _interceptors.Add(interceptor);
        }

        /// <summary>
        /// 移除拦截器
        /// </summary>
        /// <param name="interceptor"></param>
        public void Remove(IDbWorkInterceptor interceptor)
        {
            _interceptors.Remove(interceptor);
        }

        /// <summary>
        /// 清空拦截器
        /// </summary>
        public void Clear()
        {
            _interceptors.Clear();
        }
    }
}
