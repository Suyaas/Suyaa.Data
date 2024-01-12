using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Helpers;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Maintenances.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.Usables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection.Emit;
using System.Text;

namespace Suyaa.Data.Maintenances
{
    /// <summary>
    /// 数据库维护
    /// </summary>
    public sealed class DbMaintenance : IDbMaintenance
    {
        private readonly IDbWorkManager _dbWorkManager;
        private IDbWork? _dbWork;
        private IDbMaintenanceProvider? _dbMaintenanceProvider;

        /// <summary>
        /// 数据库维护
        /// </summary>
        /// <param name="dbWorkManager"></param>
        public DbMaintenance(
            IDbWorkManager dbWorkManager
            )
        {
            _dbWorkManager = dbWorkManager;
        }

        /// <summary>
        /// 获取维护供应商
        /// </summary>
        /// <returns></returns>
        private IDbMaintenanceProvider GetMaintenanceProvider(IDbWork work)
        {
            _dbMaintenanceProvider ??= work.ConnectionDescriptor.DatabaseType.GetDbProvider().MaintenanceProvider;
            return _dbMaintenanceProvider;
        }

        /// <summary>
        /// 获取数据库作业
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotExistException{IDbWork}"></exception>
        public IDbWork GetDbWork()
        {
            _dbWork ??= _dbWorkManager.GetCurrentWork();
            if (_dbWork is null) throw new NotExistException<IDbWork>();
            return _dbWork;
        }

        /// <summary>
        /// 检测Schema是否存在
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public bool CheckSchemaExists(string schema)
        {
            var work = GetDbWork();
            return GetMaintenanceProvider(work).CheckSchemaExists(work, schema);
        }

        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool CheckTableExists(string schema, string table)
        {
            var work = GetDbWork();
            return GetMaintenanceProvider(work).CheckTableExists(work, schema, table);
        }

        /// <summary>
        /// 检测字段是否存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool CheckColumnExists(string schema, string table, string column)
        {
            var work = GetDbWork();
            return GetMaintenanceProvider(work).CheckColumnExists(work, schema, table, column);
        }

        /// <summary>
        /// 获取所有Schema
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSchemas()
        {
            var work = GetDbWork();
            return GetMaintenanceProvider(work).GetSchemas(work);
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public IEnumerable<string> GetTables(string schema)
        {
            var work = GetDbWork();
            return GetMaintenanceProvider(work).GetTables(work, schema);
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public IEnumerable<string> GetColumns(string schema, string table)
        {
            var work = GetDbWork();
            return GetMaintenanceProvider(work).GetColumns(work, schema, table);
        }

        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetColumnDataType(string schema, string table, string column)
        {
            var work = GetDbWork();
            return GetMaintenanceProvider(work).GetColumnDataType(work, schema, table, column);
        }

        /// <summary>
        /// 创建Schema
        /// </summary>
        /// <param name="schema"></param>
        public void CreateSchema(string schema)
        {
            var work = GetDbWork();
            GetMaintenanceProvider(work).CreateSchema(work, schema);
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="entity"></param>
        public void CreateTable(DbEntityModel entity)
        {
            var work = GetDbWork();
            GetMaintenanceProvider(work).CreateTable(work, entity);
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        public void CreateColumn(DbEntityModel entity, ColumnModel column)
        {
            var work = GetDbWork();
            GetMaintenanceProvider(work).CreateColumn(work, entity, column);
        }

        /// <summary>
        /// 更新字段类型
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        public void UpdateColumnType(DbEntityModel entity, ColumnModel column)
        {
            var work = GetDbWork();
            GetMaintenanceProvider(work).UpdateColumnType(work, entity, column);
        }
    }
}
