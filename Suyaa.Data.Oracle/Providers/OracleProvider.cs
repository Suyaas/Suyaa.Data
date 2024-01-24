using Oracle.ManagedDataAccess.Client;
using Suyaa.Data.Kernel.Dependency;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Oracle.Maintenances.Providers;
using Suyaa.Data.Repositories.Dependency;
using System.Data.Common;

namespace Suyaa.Data.Oracle.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class OracleProvider : IDbProvider
    {
        private OracleScriptProvider? _scriptProvider;
        private OracleExecuteProvider? _executeProvider;
        private IDbMaintenanceProvider? _maintenanceProvider;

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider ScriptProvider => _scriptProvider ??= new OracleScriptProvider();

        /// <summary>
        /// 执行供应商
        /// </summary>
        public IDbExecuteProvider ExecuteProvider => _executeProvider ??= new OracleExecuteProvider();

        /// <summary>
        /// 维护供应商
        /// </summary>
        public IDbMaintenanceProvider MaintenanceProvider => _maintenanceProvider ??= new OracleMaintenanceProvider();

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
