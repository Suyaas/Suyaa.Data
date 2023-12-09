using Suyaa.EFCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Dependency
{
    /// <summary>
    /// DbSet 供应商
    /// </summary>
    public interface IDbSetProvider
    {
        /// <summary>
        /// 获取 DbSet 描述集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<DbSetModel> GetDbSets();
    }
}
