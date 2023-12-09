using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using Suyaa.EFCore.Contexts;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.Models
{
    /// <summary>
    /// DbSet 描述
    /// </summary>
    public class DbSetModel : DbEntityModel
    {
        /// <summary>
        /// DbSet 描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <param name="dbContext"></param>
        public DbSetModel(Type type, PropertyInfo property, IDescriptorDbContext dbContext) : base(type)
        {
            Property = property;
            DbContext = dbContext.GetType();
            ConnectionDescriptor = dbContext.ConnectionDescriptor;
        }

        /// <summary>
        /// DbSet 描述
        /// </summary>
        /// <param name="type"></param>
        /// <param name="property"></param>
        /// <param name="dbContextType"></param>
        /// <param name="connectionDescriptor"></param>
        public DbSetModel(Type type, PropertyInfo property, Type dbContextType, IDbConnectionDescriptor connectionDescriptor) : base(type)
        {
            Property = property;
            DbContext = dbContextType;
            ConnectionDescriptor = connectionDescriptor;
        }

        /// <summary>
        /// DbSet 属性信息
        /// </summary>
        public PropertyInfo Property { get; }

        /// <summary>
        /// 数据库连接描述
        /// </summary>
        public Type DbContext { get; }

        /// <summary>
        /// 数据库连接描述
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor { get; }
    }

}
