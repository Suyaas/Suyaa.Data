using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.EFCore.Sources
{
    /// <summary>
    /// DbSet建模源
    /// </summary>
    public class DbSetModelSource : IEntityModelSource
    {
        /// <summary>
        /// 类型建模源
        /// </summary>
        public DbSetModelSource(
            Type type,
            PropertyInfo propertyInfo,
            IDefineDbContext dbContext
            )
        {
            if (!type.HasInterface<IDbEntity>()) throw new TypeNotSupportedException(type);
            Type = type;
            PropertyInfo = propertyInfo;
            DbContext = dbContext;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public IDefineDbContext DbContext { get; }
    }
}
