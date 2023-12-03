using Npgsql;
using Suyaa.Data.Dependency;
using Suyaa.Data.PostgreSQL.Repositories;
using System.Data.Common;

namespace Suyaa.Data.PostgreSQL.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class PostgreSqlProvider : IDbProvider
    {
        private readonly IDbWorkProvider _dbWorkProvider;
        private PostgreSqlQueryProvider? _queryProvider;
        private PostgreSqlScriptProvider? _scriptProvider;

        /// <summary>
        /// Oracle数据库供应商
        /// </summary>
        public PostgreSqlProvider(
            IDbWorkProvider dbWorkProvider
            )
        {
            _dbWorkProvider = dbWorkProvider;
        }

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IDbQueryProvider QueryProvider => _queryProvider ??= new PostgreSqlQueryProvider();

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider ScriptProvider => _scriptProvider ??= new PostgreSqlScriptProvider();

        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <returns></returns>
        public DbConnection GetDbConnection()
        {
            var work = _dbWorkProvider.GetCurrentWork();
            if (work is null) throw new DbException("Current db work not found.");
            var dbc = new NpgsqlConnection(work.ConnectionDescriptor.ToConnectionString());
            dbc.Open();
            return dbc;
        }

        /// <summary>
        /// 获取一个Sql仓库
        /// </summary>
        /// <returns></returns>
        public ISqlRepository GetSqlRepository()
        {
            return new PostgreSqlRepository(_dbWorkProvider);
        }
    }
}
