using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Enums;
using Suyaa.Data.Helpers;
using SuyaaTest.PostgreSQL.Entities;
using SuyaaTest.PostgreSQL.ModelConventions;
using System.Configuration;
using Xunit.Abstractions;

namespace SuyaaTest.PostgreSQL
{
    /// <summary>
    /// 单元测试
    /// </summary>
    public class UnitTest1
    {
        private const string _connectionString = "server=10.10.100.11;port=5432;database=yan_xin;username=dbadmin;password=123456";

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
        public void ReadData()
        {
            using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.EfCore.CreateWork(dbContext);
            var repository = sy.EfCore.CreateRepository<Test, string>(work);
            var query = from p in repository.Query()
                        where p.Content != null
                        select p;
            var data = query.AsNoTracking().FirstOrDefault();
            _output.WriteLine("Content: " + data?.Content);
        }

        /// <summary>
        /// 连接
        /// </summary>
        [Fact]
        public void InsertEFCore()
        {
            sy.EfCore.UseModelConvention(new LowercaseUnderlinedModelConvention());
            using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.EfCore.CreateWork(dbContext);
            var repository = sy.EfCore.CreateRepository<Test, string>(work);
            repository.Insert(new Test() { Content = "Test001" });
            work.Commit();
            _output.WriteLine("OK");
        }

        /// <summary>
        /// 连接
        /// </summary>
        [Fact]
        public void InsertDb()
        {
            sy.Data.UseModelConvention(new LowercaseUnderlinedModelConvention());
            //using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connectionString);
            var repository = sy.Data.CreateRepository<Test, string>(work);
            repository.Insert(new Test() { Content = "Test002" });
            work.Commit();
            _output.WriteLine("OK");
        }
    }
}