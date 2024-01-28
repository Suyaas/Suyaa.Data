using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Suyaa.Data.Compilers.Sets
{
    /// <summary>
    /// 值类型
    /// </summary>
    [Description("值类型")]
    public enum ValueType
    {
        /// <summary>
        /// 空
        /// </summary>
        [Description("空")]
        Null = 0x0,
        /// <summary>
        /// 常规值
        /// </summary>
        [Description("常规值")]
        RegularValue = 0x1,
        /// <summary>
        /// 字符串值
        /// </summary>
        [Description("字符串值")]
        StringValue = 0x2,
        /// <summary>
        /// 表达式
        /// </summary>
        [Description("表达式")]
        Expression = 0x10,
        /// <summary>
        /// 对象
        /// </summary>
        [Description("对象")]
        Object = 0x20,
    }

    /// <summary>
    /// 值设定
    /// </summary>
    public class ValueSet
    {
        /// <summary>
        /// 值设定
        /// </summary>
        /// <param name="valueType"></param>
        /// <param name="value"></param>
        public ValueSet(ValueType valueType, object? value)
        {
            ValueType = valueType;
            Value = value;
        }
        /// <summary>
        /// 值类型
        /// </summary>
        public ValueType ValueType { get; }
        /// <summary>
        /// 值
        /// </summary>
        public object? Value { get; }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <returns></returns>
        public new string ToString()
        {
            if (Value is null) return string.Empty;
            return Convert.ToString(Value);
        }

        #region 静态方法
        static ValueSet()
        {
            Null = new ValueSet(ValueType.Null, null);
        }
        /// <summary>
        /// 空值设定
        /// </summary>
        public static ValueSet Null { get; }
        /// <summary>
        /// 创建值设定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ValueSet<T> Create<T>(ValueType valueType, T value)
        {
            return new ValueSet<T>(valueType, value);
        }
        #endregion
    }

    /// <summary>
    /// 值设定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueSet<T> : ValueSet
    {
        /// <summary>
        /// 值设定
        /// </summary>
        /// <param name="valueType"></param>
        /// <param name="value"></param>
        public ValueSet(ValueType valueType, T value) : base(valueType, value)
        {
        }
        /// <summary>
        /// 值
        /// </summary>
        public new T Value
        {
            get => (T)base.Value ?? throw new NullException<T>();
        }
    }
}
