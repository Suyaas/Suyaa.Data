using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Sqlite.Tests.Entities;
using Xunit.Abstractions;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using Suyaa.EFCore.Dependency;
using Suyaa.Data.Queries;
using Suyaa.Data.Sqlite;
using Suyaa.EFCore;
using SqlServerDemo.Entities;
using Suyaa.Data.Enums;
using Suyaa.EFCore.Contexts;
using Suyaa.Data.Expressions;
using Suyaa.Data.Maintenances.Helpers;
using Suyaa.Data.Sqlite.Helpers;

namespace Suyaa.Sqlite.Tests
{
    public class Test
    {
        private readonly ITestOutputHelper _output;

        public Test(ITestOutputHelper testOutput)
        {
            _output = testOutput;
        }

        [Fact]
        public void GetTables()
        {
            // ��������
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // ������ҵ
            using var work = sy.Data.CreateWork(DatabaseType.Sqlite, connectionString);
            // �������ݿ�ά������
            var maintenance = work.GetMaintenance();
            var tables = maintenance.GetTables();
            //var repository = sy.Data.CreateRepository<Test, string>(work);
            //repository.Delete(d => DbValue.IsNull(d.CreationTime));
            //work.Commit();
            foreach (var table in tables)
            {
                _output.WriteLine(table);
            }
        }



        //[Fact]
        //public void Insert()
        //{
        //    // ��������
        //    string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
        //    // ִ�з���
        //    using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
        //    {
        //        conn.Open();
        //        Data.Dependency.IRepository<People, string> peopleRepository = new Data.Dependency.Repositories.Repository<People, string>(conn);
        //        Data.Dependency.IRepository<Department, string> departmentRepository = new Data.Dependency.Repositories.Repository<Department, string>(conn);
        //        // ��Ӵ���
        //        Department? bigDepartment = departmentRepository.GetRow(d => d.Name == "����");
        //        if (bigDepartment is null)
        //        {
        //            bigDepartment = new Department()
        //            {
        //                Name = "����"
        //            };
        //            departmentRepository.Insert(bigDepartment);
        //        }
        //        // ���С����
        //        Department? smallDepartment = departmentRepository.GetRow(d => d.Name == "С����");
        //        if (smallDepartment is null)
        //        {
        //            smallDepartment = new Department()
        //            {
        //                Name = "С����"
        //            };
        //            departmentRepository.Insert(smallDepartment);
        //        }
        //        // �������
        //        if (peopleRepository.GetRow(d => d.Name == "����") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 10,
        //                Name = "����",
        //                DepartmentId = bigDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //        // �������
        //        if (peopleRepository.GetRow(d => d.Name == "����") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 20,
        //                Name = "����",
        //                DepartmentId = bigDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //        // �������
        //        if (peopleRepository.GetRow(d => d.Name == "����") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 30,
        //                Name = "����",
        //                DepartmentId = smallDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //    }
        //    // ���ؽ��
        //    _output.WriteLine("OK");
        //}

        [Fact]
        public void Query()
        {
            //// ��������
            //string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            //// ִ�з���
            //using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            //{
            //    conn.Open();
            //    Data.Dependency.IRepository<People, string> repository = new Data.Dependency.Repositories.Repository<People, string>(conn);
            //    var peoples = repository.GetRows(d => d.Age > 8);
            //    // ���ؽ��
            //    _output.WriteLine($"peoples: {peoples.Count}");
            //}
            //var provider = new SqliteProvider();
            //var department = new EntityQueryable<Department>(new EntityQueryProvider(provider.QueryProvider));
            //var query = from d in department
            //            select d;
            //var list = query.ToList();
        }

        [Fact]
        public void EFQuery()
        {
            // ��������
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            var optionsBuilder = new DbContextOptionsBuilder<Microsoft.EntityFrameworkCore.DbContext>();
            optionsBuilder.UseSqlite(connectionString);
            // ִ�з���
            //using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
            //{
            //    //IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
            //    //IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
            //    var query = from p in context.Peoples
            //                join d in context.Departments on p.DepartmentId equals d.Id
            //                where d.Name.Contains("��")
            //                select p;
            //    var datas = await query.ToListAsync();
            //    // ���ؽ��
            //    _output.WriteLine(JsonSerializer.Serialize(datas, new JsonSerializerOptions()
            //    {
            //        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //        WriteIndented = true,
            //    }));
            //}
        }

        //[Fact]
        //public async void EFUpdate()
        //{
        //    // �����������
        //    Random random = new Random();
        //    // ��������
        //    string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
        //    var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
        //    optionsBuilder.UseSqlite(connectionString);
        //    // ִ�з���
        //    using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
        //    {
        //        IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
        //        IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
        //        var query = from p in peopleRepository.Query()
        //                    join d in departmentRepository.Query() on p.DepartmentId equals d.Id
        //                    where p.Name == "����"
        //                    select p;
        //        var data = await query.FirstOrDefaultAsync();
        //        if (data is null)
        //        {
        //            _output.WriteLine($"not found.");
        //            return;
        //        }
        //        data.Age = random.Next(100);
        //        await peopleRepository.UpdateAsync(data);
        //        // ���ؽ��
        //        _output.WriteLine($"OK");
        //    }
        //}

        [Fact]
        public void EFQueryDescriptor()
        {
            //var descriptor = new DbConnectionDescriptor("def", "[SqlServer]Server=10.10.10.32,1433;Initial Catalog=Aos_cnglj;User ID=sa;Password=123456;TrustServerCertificate=true;");
            //// �����������
            //Random random = new Random();
            //// ��������
            ////string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            ////var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            ////optionsBuilder.UseSqlite(connectionString);
            //List<Type> types = new List<Type>() {
            //    typeof(SystemTables),
            //    typeof(SystemObjects),
            //};
            //// ִ�з���
            //using var testDbContext = new TestDbContext(descriptor);
            //using var context = new DescriptorTypeDbContext(testDbContext.ConnectionDescriptor, testDbContext.Options, types);
            //{
            //    //IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
            //    //IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
            //    var query = from st in context.Set<SystemTables>()
            //                join so in context.Set<SystemObjects>() on st.ObjectID equals so.Id
            //                //where p.Name == "����"
            //                select st;
            //    var data = await query.FirstOrDefaultAsync();
            //    if (data is null)
            //    {
            //        _output.WriteLine($"not found.");
            //        return;
            //    }
            //    //data.Age = random.Next(100);
            //    //await peopleRepository.UpdateAsync(data);
            //    // ���ؽ��
            //    _output.WriteLine(data.Name);
            //}
        }
    }
}