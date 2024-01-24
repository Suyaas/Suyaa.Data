using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Helpers;
using Suyaa.Data.Kernel.Enums;
using Suyaa.Data.Maintenances.Helpers;
using Suyaa.Data.Repositories;
using SuyaaTest.Oracle.Entities;
using System.Configuration;
using Xunit.Abstractions;

namespace SuyaaTest.Oracle
{
    /// <summary>
    /// 单元测试
    /// </summary>
    public class UnitTest1
    {
        private const string ConnectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =172.16.30.121)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = mesdb)));User ID=mesdb;Password=mesdb";

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
        public void Sql()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var repo = work.GetSqlRepository();
            var table = repo.GetDataTable("SELECT * FROM PRODUCTFAMILY PF");
            // 返回结果
            _output.WriteLine("R:" + table.Rows.Count);
        }
        /// <summary>
        /// 连接
        /// </summary>
        [Fact]
        public void ReadDatas()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var repo = work.GetSqlRepository();
            var productFamilies = repo.GetDatas<ProductFamily>("SELECT * FROM PRODUCTFAMILY PF");
            // 返回结果
            foreach (var productFamily in productFamilies)
            {
                _output.WriteLine(productFamily.ProductFamilyName);
            }
        }
        /// <summary>
        /// 连接
        /// </summary>
        [Fact]
        public void ReadData()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var repo = work.GetSqlRepository();
            DbParameters keys = new DbParameters();
            keys["P_1"] = "9528";
            var productFamily = repo.GetData<ProductFamily>("SELECT * FROM PRODUCTFAMILY PF WHERE PF.PRODUCTFAMILYNAME = :P_1", keys);
            // 返回结果
            _output.WriteLine("Name:" + productFamily?.ProductFamilyName);
        }

        /// <summary>
        /// 获取所有Schema
        /// </summary>
        [Fact]
        public void GetSchemas()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var schemas = maintenance.GetSchemas();
            foreach (var schema in schemas)
            {
                _output.WriteLine(schema);
            }
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        [Fact]
        public void GetTables()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var tables = maintenance.GetTables("MESDB");
            foreach (var table in tables)
            {
                _output.WriteLine(table);
            }
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        [Fact]
        public void GetColumns()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var columns = maintenance.GetColumns("MESDB", "ACTIVITY");
            foreach (var column in columns)
            {
                _output.WriteLine(column);
            }
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        [Fact]
        public void GetColumnType()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var dataType = maintenance.GetColumnDataType("MESDB", "ACTIVITY", "ALLOWREASSIGNMENT");
            _output.WriteLine(dataType);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        [Fact]
        public void QueryList()
        {
            // 执行方法
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var repository = sy.Data.CreateRepository<ProductFamily>(work);
            //var query = from pf in repository.Query()
            //            select pf;
            var list = repository.Query().ToList();
            foreach (var item in list)
            {
                _output.WriteLine(item.ProductFamilyName);
            }
        }
    }
}