using System;
using System.Collections.Generic;
using System.Text;
using Suyaa.Data.Repositories;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 数据库作业命令
    /// </summary>
    public sealed class DbWorkCommand
    {
        /// <summary>
        /// 数据库作业命令
        /// </summary>
        public DbWorkCommand(string commandText, DbParameters parameters)
        {
            CommandText = commandText;
            Parameters = parameters;
        }

        /// <summary>
        /// 数据库作业命令包裹曾
        /// </summary>
        public DbWorkCommand(string commandText)
        {
            CommandText = commandText;
            Parameters = new DbParameters();
        }

        /// <summary>
        /// 命令文本
        /// </summary>
        public string CommandText { get; }

        /// <summary>
        /// 参数集合
        /// </summary>
        public DbParameters Parameters { get; }
    }
}
