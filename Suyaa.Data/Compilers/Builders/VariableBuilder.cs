using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Compilers.Builders
{
    /// <summary>
    /// 变量构建器
    /// </summary>
    public sealed class VariableBuilder
    {
        private static readonly object _lock = new object();
        private int _indexer = 0;

        // 获取新的索引号
        private int GetNewIndex()
        {
            int index = 0;
            lock (_lock)
            {
                index = ++_indexer;
            }
            return index;
        }

        /// <summary>
        /// 获取新的表名称
        /// </summary>
        /// <returns></returns>
        public string GetNewTableName()
        {
            return "T" + GetNewIndex();
        }

        /// <summary>
        /// 获取新的变量名称
        /// </summary>
        /// <returns></returns>
        public string GetNewVariableName()
        {
            return "V" + GetNewIndex();
        }
    }
}
