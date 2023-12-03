using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 实例对象
    /// </summary>
    public interface IEntity
    {
    }
    /// <summary>
    /// 实例对象
    /// </summary>
    public interface IEntity<TId> : IEntity where TId : notnull
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        TId Id { get; set; }
    }
}
