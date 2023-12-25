using Microsoft.Data.Sqlite;
using Suyaa.Data.Kernel.Dependency;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Sqlite.Providers
{
    /// <summary>
    /// Sqlite供应商
    /// </summary>
    public sealed class SqliteProvider : IDbProvider
    {
        private IDbExpressionProvider? _expressionProvider;
        private IDbScriptProvider? _scriptProvider;
        private IDbExecuteProvider? _executeProvider;
        private IDbMaintenanceProvider? _maintenanceProvider;

        /// <summary>
        /// 表达式供应商
        /// </summary>
        public IDbExpressionProvider ExpressionProvider => _expressionProvider ??= new SqliteExpressionProvider();

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider ScriptProvider => _scriptProvider ??= new SqliteScriptProvider();

        /// <summary>
        /// 执行供应商
        /// </summary>
        public IDbExecuteProvider ExecuteProvider => _executeProvider ??= new SqliteExecuteProvider();

        /// <summary>
        /// 维护供应商
        /// </summary>
        public IDbMaintenanceProvider MaintenanceProvider => _maintenanceProvider ??= new SqliteMaintenanceProvider();

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(string connectionString)
        {
            var dbc = new SqliteConnection(connectionString);
            dbc.Open();
            return dbc;
        }
    }
}
