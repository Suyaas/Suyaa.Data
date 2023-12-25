using Oracle.ManagedDataAccess.Client;
using Suyaa.Data.Kernel.Dependency;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System.Data.Common;

namespace Suyaa.Data.Oracle.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class OracleProvider : IDbProvider
    {
        private OracleExpressionProvider? _expressionProvider;
        private OracleScriptProvider? _scriptProvider;
        private OracleExecuteProvider? _executeProvider;

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IDbExpressionProvider ExpressionProvider => _expressionProvider ??= new OracleExpressionProvider();

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider ScriptProvider => _scriptProvider ??= new OracleScriptProvider();

        /// <summary>
        /// Sql仓库供应商
        /// </summary>
        public IDbExecuteProvider ExecuteProvider => _executeProvider ??= new OracleExecuteProvider();

        /// <summary>
        /// 维护供应商
        /// </summary>
        public IDbMaintenanceProvider MaintenanceProvider => throw new System.NotImplementedException();

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(string connectionString)
        {
            var dbc = new OracleConnection(connectionString);
            dbc.Open();
            return dbc;
        }
    }
}
