using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.EFCore.Sources
{
    /// <summary>
    /// 类型数据库上下文建模源
    /// </summary>
    public class TypeDbContextModelSource : IEntityModelSource
    {
        /// <summary>
        /// 类型建模源
        /// </summary>
        public TypeDbContextModelSource(
            Type type,
            DescriptorTypeDbContext dbContext
            )
        {
            if (!type.HasInterface<IDbEntity>()) throw new TypeNotSupportedException(type);
            Type = type;
            DbContext = dbContext;
        }

        /// <summary>
        /// 类型建模源
        /// </summary>
        public TypeDbContextModelSource(
            Type type
            )
        {
            if (!type.HasInterface<IDbEntity>()) throw new TypeNotSupportedException(type);
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DescriptorTypeDbContext? DbContext { get; }
    }
}
