using Suyaa.Data;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.EFCore.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.EFCore.Helpers
{
    /// <summary>
    /// 数据库作业助手
    /// </summary>
    public static class DbWorkHelper
    {
        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public static DescriptorTypeDbContext GetDbContext(this IDbWork? work)
        {
            if (work is EfCoreWork efCoreWork) return efCoreWork.DbContext;
            if (work is null) throw new NullException<EfCoreWork>();
            throw new TypeNotSupportedException(work.GetType());
        }
    }
}
