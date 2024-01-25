using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Expressions.Dependency
{
    /// <summary>
    /// 含有别名
    /// </summary>
    public interface IHaveAlias
    {
        /// <summary>
        /// 别名
        /// </summary>
        string Alias { get; set; }
    }
}
