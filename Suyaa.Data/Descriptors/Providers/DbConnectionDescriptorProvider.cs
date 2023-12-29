﻿using Suyaa.Data.Descriptors.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Suyaa.Data.Descriptors.Providers
{
    /// <summary>
    /// 数据库连接描述供应商
    /// </summary>
    public class DbConnectionDescriptorProvider : IDbConnectionDescriptorProvider
    {
        private readonly List<IDbConnectionDescriptor> _descriptors;

        /// <summary>
        /// 数据库连接描述供应商
        /// </summary>
        public DbConnectionDescriptorProvider()
        {
            _descriptors = new List<IDbConnectionDescriptor>();
        }

        /// <summary>
        /// 获取数据库连接描述集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDbConnectionDescriptor> GetDbConnections()
        {
            return _descriptors;
        }

        /// <summary>
        /// 添加数据库连接描述
        /// </summary>
        /// <param name="dbConnectionDescriptor"></param>
        public void AddDbConnection(IDbConnectionDescriptor dbConnectionDescriptor)
        {
            if (_descriptors.Contains(dbConnectionDescriptor)) return;
            _descriptors.Add(dbConnectionDescriptor);
        }
    }
}