using Suyaa.Data;
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
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 执行方法
            using (DatabaseConnection conn = new DatabaseConnection(DbTypes.Oracle, connectionString))
            {
                conn.Open();
            }
            // 返回结果
            _output.WriteLine("OK");
        }
    }
}