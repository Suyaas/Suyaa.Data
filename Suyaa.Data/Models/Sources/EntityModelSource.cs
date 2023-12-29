using Suyaa.Data.Models.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Models.Sources
{
    /// <summary>
    /// 类型建模源
    /// </summary>
    public class EntityModelSource : IEntityModelSource
    {
        /// <summary>
        /// 类型建模源
        /// </summary>
        public EntityModelSource(Type type)
        {
            Type = type;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }
    }
}
