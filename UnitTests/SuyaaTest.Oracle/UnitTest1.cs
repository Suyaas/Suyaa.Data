using Suyaa.Data;
using Suyaa.Data.Helpers;
using Xunit.Abstractions;

namespace SuyaaTest.Oracle
{
    /// <summary>
    /// 单元测试
    /// </summary>
    public class UnitTest1
    {
        #region DI注入

        private readonly ITestOutputHelper _output;

        /// <summary>
        /// 单元测试
        /// </summary>
        /// <param name="output"></param>
        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }

        #endregion

        /// <summary>
        /// 连接
        /// </summary>
        [Fact]
        public void Connect()
        {
            // 定义数据
            string connectionString = $"Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =172.16.30.121)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = mesdb)));User ID=mesdb;Password=mesdb";
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, connectionString);
            var repo = work.GetSqlRepository();
            var table = repo.GetDataTable("SELECT * FROM PRODUCTFAMILY PF");
            // 返回结果
            _output.WriteLine("R:" + table.Rows.Count);
        }
    }
}