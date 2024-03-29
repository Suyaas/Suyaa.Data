using Xunit.Abstractions;
using Suyaa.Data.Maintenances.Helpers;
using Suyaa.Data.Sqlite.Helpers;
using SuyaaTest.Sqlite.Entities;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Kernel;
using Suyaa.Data.Models;
using Suyaa.Data.Kernel.Enums;
using Suyaa.Data.Models.Providers;

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
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 创建作业
            using var work = sy.Data.CreateWork(DatabaseType.Sqlite, connectionString);
            // 创建数据库维护对象
            var maintenance = work.GetMaintenance();
            var tables = maintenance.GetTables();
            // 输出所有表名
            foreach (var table in tables)
            {
                _output.WriteLine(table);
            }
        }

        [Fact]
        public void GetTableColumns()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 创建作业
            using var work = sy.Data.CreateWork(DatabaseType.Sqlite, connectionString);
            // 创建数据库维护对象
            var maintenance = work.GetMaintenance();
            // 创建数据库工厂
            var dbFactory = new DbFactory();
            // 实体建模约定器
            var entityModelConventions = Enumerable.Empty<IEntityModelConvention>();
            // 创建工厂
            var entityModelFactory = new EntityModelFactory(new List<IEntityModelProvider>() {
                new EntityModelProvider(dbFactory, entityModelConventions),
                new DbEntityModelProvider(dbFactory, entityModelConventions),
            });
            // 获取实体建模
            var entity = entityModelFactory.GetDbEntity<VersionInfo>();
            // 获取所有字段
            var columns = maintenance.GetColumns(entity.Name);
            // 输出所有表名
            foreach (var column in columns)
            {
                _output.WriteLine(column);
            }
        }

        [Fact]
        public void CreateTable()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 创建作业
            using var work = sy.Data.CreateWork(DatabaseType.Sqlite, connectionString);
            // 创建数据库维护对象
            var maintenance = work.GetMaintenance();
            // 创建数据库工厂
            var dbFactory = new DbFactory();
            // 实体建模约定器
            var entityModelConventions = Enumerable.Empty<IEntityModelConvention>();
            // 创建工厂
            var entityModelFactory = new EntityModelFactory(new List<IEntityModelProvider>() {
                new EntityModelProvider(dbFactory, entityModelConventions),
                new DbEntityModelProvider(dbFactory, entityModelConventions),
            });
            // 获取实体建模
            var entity = entityModelFactory.GetDbEntity<VersionInfo>();
            if (!maintenance.CheckTableExists(entity.Name))
            {
                maintenance.CreateTable(entity);
                work.Commit();
            }
            _output.WriteLine("OK");
        }

        [Fact]
        public void CreateTableColumn()
        {
            // 定义数据
            string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            // 创建作业
            using var work = sy.Data.CreateWork(DatabaseType.Sqlite, connectionString);
            // 创建数据库维护对象
            var maintenance = work.GetMaintenance();
            // 创建数据库工厂
            var dbFactory = new DbFactory();
            // 实体建模约定器
            var entityModelConventions = Enumerable.Empty<IEntityModelConvention>();
            // 创建工厂
            var entityModelFactory = new EntityModelFactory(new List<IEntityModelProvider>() {
                new EntityModelProvider(dbFactory, entityModelConventions),
                new DbEntityModelProvider(dbFactory, entityModelConventions),
            });
            // 获取实体建模
            var entity = entityModelFactory.GetDbEntity<VersionInfo>();
            if (maintenance.CheckTableExists(entity.Name))
            {
                foreach (var column in entity.Columns)
                {
                    if (!maintenance.CheckColumnExists(entity.Name, column.Name))
                    {
                        maintenance.CreateColumn(entity, column);
                    }
                }
                work.Commit();
            }
            _output.WriteLine("OK");
        }

        //[Fact]
        //public void Insert()
        //{
        //    // 定义数据
        //    string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
        //    // 执行方法
        //    using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
        //    {
        //        conn.Open();
        //        Data.Dependency.IRepository<People, string> peopleRepository = new Data.Dependency.Repositories.Repository<People, string>(conn);
        //        Data.Dependency.IRepository<Department, string> departmentRepository = new Data.Dependency.Repositories.Repository<Department, string>(conn);
        //        // 添加大部门
        //        Department? bigDepartment = departmentRepository.GetRow(d => d.Name == "大部门");
        //        if (bigDepartment is null)
        //        {
        //            bigDepartment = new Department()
        //            {
        //                Name = "大部门"
        //            };
        //            departmentRepository.Insert(bigDepartment);
        //        }
        //        // 添加小部门
        //        Department? smallDepartment = departmentRepository.GetRow(d => d.Name == "小部门");
        //        if (smallDepartment is null)
        //        {
        //            smallDepartment = new Department()
        //            {
        //                Name = "小部门"
        //            };
        //            departmentRepository.Insert(smallDepartment);
        //        }
        //        // 添加张三
        //        if (peopleRepository.GetRow(d => d.Name == "张三") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 10,
        //                Name = "张三",
        //                DepartmentId = bigDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //        // 添加李四
        //        if (peopleRepository.GetRow(d => d.Name == "李四") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 20,
        //                Name = "李四",
        //                DepartmentId = bigDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //        // 添加王五
        //        if (peopleRepository.GetRow(d => d.Name == "王五") is null)
        //        {
        //            People people = new People()
        //            {
        //                Age = 30,
        //                Name = "王五",
        //                DepartmentId = smallDepartment.Id,
        //            };
        //            peopleRepository.Insert(people);
        //        }
        //    }
        //    // 返回结果
        //    _output.WriteLine("OK");
        //}

        [Fact]
        public void Query()
        {
            //// 定义数据
            //string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            //// 执行方法
            //using (DatabaseConnection conn = new DatabaseConnection(DatabaseTypes.Sqlite, connectionString))
            //{
            //    conn.Open();
            //    Data.Dependency.IRepository<People, string> repository = new Data.Dependency.Repositories.Repository<People, string>(conn);
            //    var peoples = repository.GetRows(d => d.Age > 8);
            //    // 返回结果
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
            //// 定义数据
            //string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            //var optionsBuilder = new DbContextOptionsBuilder<Microsoft.EntityFrameworkCore.DbContext>();
            //optionsBuilder.UseSqlite(connectionString);
            // 执行方法
            //using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
            //{
            //    //IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
            //    //IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
            //    var query = from p in context.Peoples
            //                join d in context.Departments on p.DepartmentId equals d.Id
            //                where d.Name.Contains("大")
            //                select p;
            //    var datas = await query.ToListAsync();
            //    // 返回结果
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
        //    // 定义随机函数
        //    Random random = new Random();
        //    // 定义数据
        //    string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
        //    var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
        //    optionsBuilder.UseSqlite(connectionString);
        //    // 执行方法
        //    using (TestDbContext context = new TestDbContext(optionsBuilder.Options, connectionString))
        //    {
        //        IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
        //        IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
        //        var query = from p in peopleRepository.Query()
        //                    join d in departmentRepository.Query() on p.DepartmentId equals d.Id
        //                    where p.Name == "张三"
        //                    select p;
        //        var data = await query.FirstOrDefaultAsync();
        //        if (data is null)
        //        {
        //            _output.WriteLine($"not found.");
        //            return;
        //        }
        //        data.Age = random.Next(100);
        //        await peopleRepository.UpdateAsync(data);
        //        // 返回结果
        //        _output.WriteLine($"OK");
        //    }
        //}

        [Fact]
        public void EFQueryDescriptor()
        {
            //var descriptor = new DbConnectionDescriptor("def", "[SqlServer]Server=10.10.10.32,1433;Initial Catalog=Aos_cnglj;User ID=sa;Password=123456;TrustServerCertificate=true;");
            //// 定义随机函数
            //Random random = new Random();
            //// 定义数据
            ////string connectionString = $"data source={sy.IO.GetExecutionPath("temp.db")}";
            ////var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
            ////optionsBuilder.UseSqlite(connectionString);
            //List<Type> types = new List<Type>() {
            //    typeof(SystemTables),
            //    typeof(SystemObjects),
            //};
            //// 执行方法
            //using var testDbContext = new TestDbContext(descriptor);
            //using var context = new DescriptorTypeDbContext(testDbContext.ConnectionDescriptor, testDbContext.Options, types);
            //{
            //    //IRepository<People, string> peopleRepository = new EFCore.Dbsets.Repository<People, string>(context);
            //    //IRepository<Department, string> departmentRepository = new EFCore.Dbsets.Repository<Department, string>(context);
            //    var query = from st in context.Set<SystemTables>()
            //                join so in context.Set<SystemObjects>() on st.ObjectID equals so.Id
            //                //where p.Name == "张三"
            //                select st;
            //    var data = await query.FirstOrDefaultAsync();
            //    if (data is null)
            //    {
            //        _output.WriteLine($"not found.");
            //        return;
            //    }
            //    //data.Age = random.Next(100);
            //    //await peopleRepository.UpdateAsync(data);
            //    // 返回结果
            //    _output.WriteLine(data.Name);
            //}
        }
    }
}