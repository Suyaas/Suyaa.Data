using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Descriptors
{
    /// <summary>
    /// DbSet 描述
    /// </summary>
    public class DbSetDescriptor : EntityDescriptor
    {
        /// <summary>
        /// DbSet 描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <param name="connectionDescriptor"></param>
        public DbSetDescriptor(Type type, PropertyInfo property, IDbConnectionDescriptor connectionDescriptor) : base(type)
        {
            Property = property;
            ConnectionDescriptor = connectionDescriptor;
        }

        /// <summary>
        /// DbSet 属性信息
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// 数据库连接描述
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor { get; }
    }
}
