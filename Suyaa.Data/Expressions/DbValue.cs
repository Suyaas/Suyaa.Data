using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Expressions
{
    /// <summary>
    /// 数据库值操作
    /// </summary>
    public static class DbValue
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNull(object? value)
        {
            return value is null;
        }

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNull(object? value)
        {
            return !(value is null);
        }
    }
}
