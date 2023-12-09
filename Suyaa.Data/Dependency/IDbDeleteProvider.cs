using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库删除操作供应商
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbDeleteProvider<TEntity> : IDbDeletable<TEntity>
        where TEntity : IEntity
    {
    }
}
