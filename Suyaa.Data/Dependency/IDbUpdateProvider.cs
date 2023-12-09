using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库更新操作供应商
    /// </summary>
    public interface IDbUpdateProvider<TEntity> : IDbUpdatable<TEntity>
        where TEntity : IEntity
    {
    }
}
