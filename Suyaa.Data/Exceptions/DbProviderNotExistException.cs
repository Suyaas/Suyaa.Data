using Suyaa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 异常 - 数据库供应商未找到
    /// </summary>
    public class DbProviderNotExistException : DbException
    {
        /// <summary>
        /// 数据库供应商未找到
        /// </summary>
        public const string KEY_DB_PROVIDER_NOT_EXIST = "Provider.NotExist";

        /// <summary>
        /// 异常 - 数据库供应商未找到
        /// </summary>
        /// <param name="name"></param>
        public DbProviderNotExistException(string name) : base(KEY_DB_PROVIDER_NOT_EXIST, "Database provider '{0}' not found.", name)
        {
        }
    }
}
