using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Sqlite.Tests.Entities;
using Xunit.Abstractions;

namespace Suyaa.PostgreSQL.Tests
{
    public class Test
    {
        private const string _connString = "server=10.10.100.11;port=5432;database=yan_xin;username=dbadmin;password=123456";

        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void Create()
        {
            // 执行方法
            using (DatabaseConnection conn = new DatabaseConnection(DatabaseType.PostgreSQL, _connString))
            {
                conn.Open();
                conn.TableCreated<People>().Wait();
            }
            // 返回结果
            _output.WriteLine("OK");
        }

        [Fact]
        public void Insert()
        {
            using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connString);
            var repository = work.GetRepository<People, string>();
            repository.Insert(new People() { Age = 10, Name = "张三" });
            work.Commit();
        }

        [Fact]
        public void Delete()
        {
            using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connString);
            var repository = work.GetRepository<People, string>();
            repository.Delete(d => d.Age < 10);
            work.Commit();
        }

        [Fact]
        public void Update()
        {
            using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connString);
            var repository = work.GetRepository<People, string>();
            repository.Update(new People() { Age = 10, Name = "张三" }, d => d.Age < 10);
            work.Commit();
        }

        [Fact]
        public void UpdateFields()
        {
            using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connString);
            var repository = work.GetRepository<People, string>();
            repository.Update(new People() { Age = 10 }, d => d.Age, d => d.Age < 10);
            work.Commit();
        }
    }
}