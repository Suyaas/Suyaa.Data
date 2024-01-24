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
    /// ��Ԫ����
    /// </summary>
    public class UnitTest1
    {
        private const string ConnectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =172.16.30.121)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = mesdb)));User ID=mesdb;Password=mesdb";

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
        public void Sql()
        {
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var repo = work.GetSqlRepository();
            var table = repo.GetDataTable("SELECT * FROM PRODUCTFAMILY PF");
            // ���ؽ��
            _output.WriteLine("R:" + table.Rows.Count);
        }
        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void ReadDatas()
        {
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var repo = work.GetSqlRepository();
            var productFamilies = repo.GetDatas<ProductFamily>("SELECT * FROM PRODUCTFAMILY PF");
            // ���ؽ��
            foreach (var productFamily in productFamilies)
            {
                _output.WriteLine(productFamily.ProductFamilyName);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        [Fact]
        public void ReadData()
        {
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var repo = work.GetSqlRepository();
            DbParameters keys = new DbParameters();
            keys["P_1"] = "9528";
            var productFamily = repo.GetData<ProductFamily>("SELECT * FROM PRODUCTFAMILY PF WHERE PF.PRODUCTFAMILYNAME = :P_1", keys);
            // ���ؽ��
            _output.WriteLine("Name:" + productFamily?.ProductFamilyName);
        }

        /// <summary>
        /// ��ȡ����Schema
        /// </summary>
        [Fact]
        public void GetSchemas()
        {
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var schemas = maintenance.GetSchemas();
            foreach (var schema in schemas)
            {
                _output.WriteLine(schema);
            }
        }

        /// <summary>
        /// ��ȡ���б�
        /// </summary>
        [Fact]
        public void GetTables()
        {
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var tables = maintenance.GetTables("MESDB");
            foreach (var table in tables)
            {
                _output.WriteLine(table);
            }
        }

        /// <summary>
        /// ��ȡ���б�
        /// </summary>
        [Fact]
        public void GetColumns()
        {
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var columns = maintenance.GetColumns("MESDB", "ACTIVITY");
            foreach (var column in columns)
            {
                _output.WriteLine(column);
            }
        }

        /// <summary>
        /// ��ȡ���б�
        /// </summary>
        [Fact]
        public void GetColumnType()
        {
            // ִ�з���
            using var work = sy.Data.CreateWork(DatabaseType.Oracle, ConnectionString);
            var maintenance = work.GetMaintenance();
            var dataType = maintenance.GetColumnDataType("MESDB", "ACTIVITY", "ALLOWREASSIGNMENT");
            _output.WriteLine(dataType);
        }

        /// <summary>
        /// ��ѯ�б�
        /// </summary>
        [Fact]
        public void QueryList()
        {
            // ִ�з���
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