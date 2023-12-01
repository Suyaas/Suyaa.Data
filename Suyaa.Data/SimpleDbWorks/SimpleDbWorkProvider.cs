using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.Data.SimpleDbWorks
{
    /// <summary>
    /// 简单的数据库工作者供应商
    /// </summary>
    public sealed class SimpleDbWorkProvider : IDbWorkProvider
    {
        private IDbWork? _work;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public SimpleDbWorkProvider()
        {
        }

        /// <summary>
        /// 创建一个工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork(DbConnectionDescriptor dbConnectionDescriptor)
        {
            return new SimpleDbWork(this, dbConnectionDescriptor);
        }

        /// <summary>
        /// 获取当前工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork? GetCurrentWork()
        {
            return _work;
        }

        /// <summary>
        /// 获取数据库供应商
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public IDbProvider GetDbProvider(DatabaseType databaseType)
        {
            return databaseType.GetDbProvider(this);
        }

        /// <summary>
        /// 释放工作者
        /// </summary>
        public void ReleaseWork()
        {
            if (_work is null) return;
            _work.Dispose();
            _work = null;
        }

        /// <summary>
        /// 设置工作者
        /// </summary>
        /// <param name="work"></param>
        public void SetCurrentWork(IDbWork work)
        {
            _work = work;
        }
    }
}
