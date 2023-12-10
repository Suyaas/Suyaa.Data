using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文供应商
    /// </summary>
    public interface IDbContextProvider
    {
        /// <summary>
        /// 获取数据库上下文集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDescriptorDbContext> GetDbContexts();
    }
}
