using Suyaa.Data.Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 异常 - 不支持的数据库类型
    /// </summary>
    public class DbTypeNotSupportedException : DbException
    {
        /// <summary>
        /// 不支持的数据库类型
        /// </summary>
        public const string KEY_DB_TYPE_NOT_SUPPORTED = "Type.NotSupported";

        /// <summary>
        /// 异常 - 不支持的数据库类型
        /// </summary>
        /// <param name="databaseType"></param>
        public DbTypeNotSupportedException(DatabaseType databaseType) : base(KEY_DB_TYPE_NOT_SUPPORTED, "DatabaseType '{0}' not supported.", databaseType.ToString())
        {
        }
    }
}
