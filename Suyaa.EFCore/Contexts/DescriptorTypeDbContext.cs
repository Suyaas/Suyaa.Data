using Microsoft.EntityFrameworkCore;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Contexts
{
    /// <summary>
    /// 带描述和类型集合数据库上下文
    /// </summary>
    public sealed class DescriptorTypeDbContext : DescriptorDbContext
    {
        // 所有类型
        private readonly IEnumerable<Type> _types;

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="entityModelConventionFactory"></param>
        /// <param name="types"></param>
        public DescriptorTypeDbContext(IDbConnectionDescriptor descriptor, IEntityModelConventionFactory entityModelConventionFactory, IEnumerable<Type> types) : base(descriptor, entityModelConventionFactory)
        {
            _types = types;
        }

        /// <summary>
        /// 动态数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="types"></param>
        public DescriptorTypeDbContext(IDbConnectionDescriptor descriptor, IEnumerable<Type> types)
            : base(descriptor, new EntityModelConventionFactory(Enumerable.Empty<IEntityModelConvention>()))
        {
            _types = types;
        }

        /// <summary>
        /// 创建模型构建器
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in _types)
            {
                modelBuilder.Entity(type);
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
