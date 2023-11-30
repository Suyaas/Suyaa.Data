using Suyaa.Data;
using Suyaa.Data.Helpers;
using Xunit.Abstractions;

namespace SuyaaTest.Oracle
{
    /// <summary>
    /// ��Ԫ����
    /// </summary>
    public class UnitTest1
    {
        #region DIע��

        private readonly ITestOutputHelper _output;

        /// <summary>
        /// ��Ԫ����
        /// </summary>
        /// <param name="output"></param>
        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void Connect()
        {
            // ��������
            string connectionString = $"Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =172.16.30.121)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = mesdb)));User ID=mesdb;Password=mesdb";
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, connectionString);
            var repo = work.GetSqlRepository();
            var table = repo.GetDataTable("SELECT * FROM PRODUCTFAMILY PF");
            // ���ؽ��
            _output.WriteLine("R:" + table.Rows.Count);
        }
    }
}