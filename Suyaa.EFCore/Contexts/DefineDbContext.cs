using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Suyaa.Data;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.Providers;
using sy;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa.EFCore.Contexts
{
    /// <summary>
    /// 定义用的数据库上下文
    /// </summary>
    public abstract class DefineDbContext : DescriptorDbContext, IDefineDbContext
    {
        /// <summary>
        /// 定义用的数据库上下文
        /// </summary>
        /// <param name="descriptor"></param>
        /// <param name="entityModelConventionFactory"></param>
        public DefineDbContext(
            IEntityModelConventionFactory entityModelConventionFactory,
            IDbConnectionDescriptor descriptor)
            : base(
                  entityModelConventionFactory,
                  new DbContextOptionsBuilderProvider(),
                  descriptor)
        {
        }
    }
}
