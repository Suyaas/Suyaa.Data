using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 实例对象
    /// </summary>
    public interface IDbEntity
    {
    }
    /// <summary>
    /// 实例对象
    /// </summary>
    public interface IDbEntity<TId> : IDbEntity where TId : notnull
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        TId Id { get; set; }
    }
}
