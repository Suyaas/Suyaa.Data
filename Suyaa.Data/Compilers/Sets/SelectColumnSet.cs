using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Compilers.Sets
{
    /// <summary>
    /// 选择列设定
    /// </summary>
    public sealed class SelectColumnSet
    {
        /// <summary>
        /// 选择列设定
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="memberName"></param>
        public SelectColumnSet(string columnName, string memberName)
        {
            ColumnName = columnName;
            MemberName = memberName;
        }

        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        /// 成员名称
        /// </summary>
        public string MemberName { get; }
    }
}
