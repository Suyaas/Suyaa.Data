using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Repositories;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Maintenances.Helpers
{
    /// <summary>
    /// 数据库作业助手
    /// </summary>
    public static class DbWorkHelper
    {
        /// <summary>
        /// 获取数据库维护对象
        /// </summary>
        /// <returns></returns>
        public static IDbMaintenance GetMaintenance(this IDbWork work)
        {
            return new DbMaintenance(work.WorkManager);
        }
    }
}
