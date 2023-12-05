using Oracle.ManagedDataAccess.Client;
using Suyaa.Data.Dependency;
using Suyaa.Data.Oracle.Repositories;
using System;
using System.Data.Common;

namespace Suyaa.Data.Oracle.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class OracleProvider : IDbProvider
    {
        private OracleQueryProvider? _queryProvider;

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IDbQueryProvider QueryProvider => _queryProvider ??= new OracleQueryProvider();

        public IDbScriptProvider ScriptProvider => throw new NotImplementedException();

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbWorkManager"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public DbConnection GetDbConnection(IDbWorkManager dbWorkManager)
        {
            var work = dbWorkManager.GetCurrentWork();
            if (work is null) throw new NotExistException<IDbWork>();
            var dbc = new OracleConnection(work.ConnectionDescriptor.ToConnectionString());
            dbc.Open();
            return dbc;
        }

        /// <summary>
        /// 获取一个Sql仓库
        /// </summary>
        /// <param name="dbWorkManager"></param>
        /// <returns></returns>
        public ISqlRepository GetSqlRepository(IDbWorkManager dbWorkManager)
        {
            return new OracleSqlRepository(dbWorkManager);
        }
    }
}
