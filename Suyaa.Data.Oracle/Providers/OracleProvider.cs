using Oracle.ManagedDataAccess.Client;
using Suyaa.Data.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System.Data.Common;

namespace Suyaa.Data.Oracle.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class OracleProvider : IDbProvider
    {
        private OracleQueryProvider? _queryProvider;
        private OracleScriptProvider? _scriptProvider;
        private OracleSqlRepositoryProvider? _sqlRepositoryProvider;

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IDbQueryProvider QueryProvider => _queryProvider ??= new OracleQueryProvider();

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider ScriptProvider => _scriptProvider ??= new OracleScriptProvider();

        /// <summary>
        /// Sql仓库供应商
        /// </summary>
        public ISqlRepositoryProvider SqlRepositoryProvider => _sqlRepositoryProvider ??= new OracleSqlRepositoryProvider();

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
