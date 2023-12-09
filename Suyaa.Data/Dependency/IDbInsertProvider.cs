using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据新增操作供应商
    /// </summary>
    public interface IDbInsertProvider<TEntity> : IDbInsertable<TEntity>
        where TEntity : IEntity
    {
    }
}
