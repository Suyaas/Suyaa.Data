using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 数据库处理异常
    /// </summary>
    public class DbException : KeyException
    {
        /// <summary>
        /// 数据库
        /// </summary>
        public const string KEY_DB = "Db";

        /// <summary>
        /// 数据库处理异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message">异常信息</param>
        /// <param name="args"></param>
        public DbException(string key, string message, params string[] args) : base(KEY_DB + "." + key, message, args)
        {
        }

        /// <summary>
        /// 数据库处理异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="message">异常信息</param>
        /// <param name="args"></param>
        /// <param name="innerException">包含的异常</param>
        public DbException(string key, Exception innerException, string message, params string[] args) : base(KEY_DB + "." + key, innerException, message, args)
        {
        }
    }
}
