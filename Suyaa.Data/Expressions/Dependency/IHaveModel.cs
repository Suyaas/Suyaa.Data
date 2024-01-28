using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Expressions.Dependency
{
    /// <summary>
    /// 是否包含建模信息
    /// </summary>
    public interface IHaveModel
    {
        /// <summary>
        /// 表建模信息
        /// </summary>
        QueryRootModel Model { get; }
    }
}
