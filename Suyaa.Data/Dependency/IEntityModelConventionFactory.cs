using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 实体建模约定器工厂
    /// </summary>
    public interface IEntityModelConventionFactory
    {
        /// <summary>
        /// 实体建模约定器集合
        /// </summary>
        IEnumerable<IEntityModelConvention> Conventions { get; }
    }
}
