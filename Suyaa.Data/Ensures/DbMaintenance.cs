using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Ensures.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Suyaa.Data.Ensures
{
    /// <summary>
    /// 数据库维护
    /// </summary>
    public sealed class DbMaintenance : IDbMaintenance
    {
        private readonly IDbWorkManager _dbWorkManager;
        private IDbWork? _dbWork;
        private ISqlRepository? _sqlRepository;
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
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetSchemaExistsScript(schema);
            return _sqlRepository.Any(sql);
        }

        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public bool CheckTableExists(string schema, string table)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetTableExistsScript(schema, table);
            return _sqlRepository.Any(sql);
        }

        /// <summary>
        /// 检测字段是否存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public bool CheckFieldExists(string schema, string table, string field)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetFieldExistsScript(schema, table, field);
            return _sqlRepository.Any(sql);
        }

        /// <summary>
        /// 获取所有Schema
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetSchemas()
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetSchemasScript();
            return _sqlRepository.GetDatas<string>(sql);
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public IEnumerable<string> GetTables(string schema)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetTablesScript(schema);
            return _sqlRepository.GetDatas<string>(sql);
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public IEnumerable<string> GetFields(string schema, string table)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetFieldsScript(schema, table);
            return _sqlRepository.GetDatas<string>(sql);
        }

        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public string GetFieldType(string schema, string table, string field)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetFieldTypeScript(schema, table, field);
            return _sqlRepository.GetData<string>(sql) ?? string.Empty;
        }

        /// <summary>
        /// 创建Schema
        /// </summary>
        /// <param name="schema"></param>
        public void CreateSchema(string schema)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetSchemaCreateScript(schema);
            _sqlRepository.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="entity"></param>
        public void CreateTable(DbEntityModel entity)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetTableCreateScript(entity);
            _sqlRepository.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        public void CreateField(DbEntityModel entity, FieldModel field)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetFieldCreateScript(entity, field);
            _sqlRepository.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新字段类型
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        public void UpdateFieldType(DbEntityModel entity, FieldModel field)
        {
            _dbWork ??= GetDbWork();
            _sqlRepository ??= _dbWork.GetSqlRepository();
            var sql = GetMaintenanceProvider(_dbWork).GetFieldTypeUpdateScript(entity, field);
            _sqlRepository.ExecuteNonQuery(sql);
        }
    }
}
