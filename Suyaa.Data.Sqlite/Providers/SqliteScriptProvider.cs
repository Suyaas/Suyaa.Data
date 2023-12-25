using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Sqlite.Providers
{
    /// <summary>
    /// Sqlite 脚本供应商
    /// </summary>
    public sealed class SqliteScriptProvider : IDbScriptProvider
    {
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetName(string name)
        {
            return "[" + name + "]";
        }

        /// <summary>
        /// 获取变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetVariable(string name)
        {
            return "$" + name;
        }
    }
}
