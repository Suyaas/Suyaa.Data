using Npgsql;
using Suyaa.Data.Kernel.Dependency;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System.Data.Common;

namespace Suyaa.Data.PostgreSQL.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class PostgreSqlProvider : IDbProvider
    {

        private PostgreSqlExpressionProvider? _expressionProvider;
        private PostgreSqlScriptProvider? _scriptProvider;
        private PostgreSqlExecuteProvider? _executeProvider;

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IDbExpressionProvider ExpressionProvider => _expressionProvider ??= new PostgreSqlExpressionProvider();

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider ScriptProvider => _scriptProvider ??= new PostgreSqlScriptProvider();

        /// <summary>
        /// Sql数据仓库
        /// </summary>
        public IDbExecuteProvider ExecuteProvider => _executeProvider ??= new PostgreSqlExecuteProvider();

        /// <summary>
        /// 维护供应商
        /// </summary>
        public IDbMaintenanceProvider MaintenanceProvider => throw new System.NotImplementedException();

        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(string connectionString)
        {
            var dbc = new NpgsqlConnection(connectionString);
            dbc.Open();
            return dbc;
        }
    }
}
