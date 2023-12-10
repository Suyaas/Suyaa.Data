using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public interface IDbContextFacotry
    {
        /// <summary>
        /// 数据库上下文工厂
        /// </summary>
        IEnumerable<IDescriptorDbContext> DbContexts { get; }
    }
}
