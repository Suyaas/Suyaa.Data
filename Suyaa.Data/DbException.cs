using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 数据库处理异常
    /// </summary>
    public class DbException : Exception
    {
        /// <summary>
        /// 原始异常消息
        /// </summary>
        public string OriginalMessage { get; }

        /// <summary>
        /// 原始参数集合
        /// </summary>
        public string[]? OriginalParameters { get; }

        /// <summary>
        /// 数据库处理异常
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="args"></param>
        public DbException(string message, params string[] args) : base(string.Format(message, args))
        {
            this.OriginalMessage = message;
            this.OriginalParameters = args;
        }

        /// <summary>
        /// 数据库处理异常
        /// </summary>
        /// <param name="message">异常信息</param>
        /// <param name="args"></param>
        /// <param name="innerException">包含的异常</param>
        public DbException(Exception innerException, string message, params string[] args) : base(string.Format(message, args), innerException) {
            this.OriginalMessage = message;
            this.OriginalParameters = args;
        }
    }
}
