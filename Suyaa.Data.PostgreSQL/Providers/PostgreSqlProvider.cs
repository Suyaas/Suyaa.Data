using Npgsql;
using Suyaa.Data.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System.Data.Common;

namespace Suyaa.Data.PostgreSQL.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class PostgreSqlProvider : IDbProvider
    {

        private PostgreSqlQueryProvider? _queryProvider;
        private PostgreSqlScriptProvider? _scriptProvider;
        private PostgreSqlRepositoryProvider? _sqlRepositoryProvider;

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IDbQueryProvider QueryProvider => _queryProvider ??= new PostgreSqlQueryProvider();

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider ScriptProvider => _scriptProvider ??= new PostgreSqlScriptProvider();

        /// <summary>
        /// Sql数据仓库
        /// </summary>
        public ISqlRepositoryProvider SqlRepositoryProvider => _sqlRepositoryProvider ??= new PostgreSqlRepositoryProvider();

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
