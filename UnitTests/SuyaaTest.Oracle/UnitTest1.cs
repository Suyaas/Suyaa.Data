using Suyaa.Data;
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
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // ִ�з���
            using (DatabaseConnection conn = new DatabaseConnection(DbTypes.Oracle, connectionString))
            {
                conn.Open();
            }
            // ���ؽ��
            _output.WriteLine("OK");
        }
    }
}