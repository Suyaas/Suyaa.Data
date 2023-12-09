using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Suyaa.EFCore.Contexts
{
    /// <summary>
    /// EFCore重写上下文
    /// </summary>
    public abstract class BaseDbContext : DbContext
    {

        /// <summary>
        /// EFCore重写上下文
        /// </summary>
        /// <param name="options"></param>
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
