using Suyaa.Data.Models.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 实体建模约定器工厂
    /// </summary>
    public class EntityModelConventionFactory : IEntityModelConventionFactory
    {
        private readonly IEnumerable<IEntityModelConvention> _conventions;

        /// <summary>
        /// 实体建模约定器工厂
        /// </summary>
        public EntityModelConventionFactory(IEnumerable<IEntityModelConvention> conventions)
        {
            _conventions = conventions;
        }

        /// <summary>
        /// 实体建模约定器集合
        /// </summary>
        public IEnumerable<IEntityModelConvention> Conventions => _conventions;
    }
}
