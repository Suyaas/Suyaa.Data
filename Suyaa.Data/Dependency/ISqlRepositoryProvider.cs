using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// SQL仓库
    /// </summary>
    public interface ISqlRepositoryProvider
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
    }
}
