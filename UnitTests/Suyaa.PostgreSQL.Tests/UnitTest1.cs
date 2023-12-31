using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.DbWorks;
using Suyaa.Data.Expressions;
using Suyaa.Data.Kernel.Enums;
using Suyaa.Data.Repositories;
using SuyaaTest.PostgreSQL.Entities;
using SuyaaTest.PostgreSQL.ModelConventions;
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
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            sy.EfCore.UseModelConvention(new LowercaseUnderlinedModelConvention());
            using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.EfCore.CreateWork(dbContext);
            var repository = sy.EfCore.CreateRepository<Test, string>(work);
            repository.Insert(new Test() { Content = "Test001" });
            repository.Insert(new Test() { Content = "Test002" });
            work.Commit();
            _output.WriteLine("OK");
        }

        /// <summary>
        /// 连接
        /// </summary>
        [Fact]
        public async void InsertEFCoreAsync()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            sy.EfCore.UseModelConvention(new LowercaseUnderlinedModelConvention());
            using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.EfCore.CreateWork(dbContext);
            var repository = sy.EfCore.CreateRepository<Test, string>(work);
            await repository.InsertAsync(new Test() { Content = "Test011" });
            await repository.InsertAsync(new Test() { Content = "Test012" });
            await work.CommitAsync();
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

        /// <summary>
        /// 更新
        /// </summary>
        [Fact]
        public void UpdateDb()
        {
            sy.Data.UseModelConvention(new LowercaseUnderlinedModelConvention());
            //using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connectionString);
            var repository = sy.Data.CreateRepository<Test, string>(work);
            var id = "111";
            Test test = new Test() { Id = "223232" };
            repository.Update(new Test() { IsDeleted = false }, d => d.IsDeleted, d => d.Id == test.Id);
            work.Commit();
            _output.WriteLine("OK");
        }

        /// <summary>
        /// 删除
        /// </summary>
        [Fact]
        public void DeleteDb()
        {
            sy.Data.UseModelConvention(new LowercaseUnderlinedModelConvention());
            //using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.Data.CreateWork(DatabaseType.PostgreSQL, _connectionString);
            var repository = sy.Data.CreateRepository<Test, string>(work);
            repository.Delete(d => DbValue.IsNull(d.CreationTime));
            work.Commit();
            _output.WriteLine("OK");
        }

        /// <summary>
        /// 连接
        /// </summary>
        [Fact]
        public void InsertMix()
        {
            // 使用拦截器
            sy.EfCore.UseWorkInterceptor(new DbWorkInterceptor());
            sy.EfCore.UseModelConvention(new LowercaseUnderlinedModelConvention());
            using var dbContext = new TestDbContext(new DbConnectionDescriptor("default", DatabaseType.PostgreSQL, _connectionString));
            using var work = sy.EfCore.CreateWork(dbContext);
            var sqlRepository = new SqlRepository(work.WorkManager);
            var repository = new MixRepository<Test, string>(work.WorkManager, sy.EfCore.GetEntityModelFactory(), sqlRepository);
            //var repository = sy.Data.CreateRepository<Test, string>(work);
            repository.Insert(new Test() { Content = "Test002" });
            work.Commit();
            _output.WriteLine("OK");
        }
    }
}