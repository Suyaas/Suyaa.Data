using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Data.DbWorks.Dependency;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// SQL仓库
    /// </summary>
    public interface IDbExecuteProvider
    {
        /// <summary>
        /// 获取一个数据库命令管理器
        /// </summary>
        /// <param name="dbWork"></param>
        /// <returns></returns>
        DbCommand GetDbCommand(IDbWork dbWork);
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="dbWork"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet GetDataSet(IDbWork dbWork, string sql);
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="dbWork"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet GetDataSet(IDbWork dbWork, string sql, DbParameters parameters);
        /// <summary>
        /// 设置参数集
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        void SetDbParameters(DbCommand command, DbParameters parameters);
    }
}
