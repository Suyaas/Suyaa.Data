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
    public interface ISqlRepository
    {
        /// <summary>
        /// 获取数据库工作者
        /// </summary>
        /// <returns></returns>
        IDbWork GetDbWork();
        /// <summary>
        /// 获取一个数据库命令管理器
        /// </summary>
        /// <returns></returns>
        DbCommand GetDbCommand();
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        DataSet GetDataSet(string sql);
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet GetDataSet(string sql, DbParameters parameters);
        /// <summary>
        /// 设置参数集
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        void SetDbParameters(DbCommand command, DbParameters parameters);
    }
}
