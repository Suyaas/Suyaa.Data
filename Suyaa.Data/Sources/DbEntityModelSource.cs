using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Sources
{
    /// <summary>
    /// 类型建模源
    /// </summary>
    public class DbEntityModelSource : IEntityModelSource
    {
        /// <summary>
        /// 类型建模源
        /// </summary>
        public DbEntityModelSource(Type type)
        {
            if (!type.HasInterface<IDbEntity>()) throw new TypeNotSupportedException(type);
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
    }
}
