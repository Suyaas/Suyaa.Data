using Suyaa.Data.Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 列类型
    /// </summary>
    public class ColumnType
    {
        /// <summary>
        /// 值类型
        /// </summary>
        public ColumnValueType Type { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// 自定义名称
        /// </summary>
        public string CustomName { get; }

        /// <summary>
        /// 列类型
        /// </summary>
        public ColumnType(string customName)
        {
            this.Type = ColumnValueType.Unknow;
            this.Length = 0;
            this.Precision = 0;
            CustomName = customName;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <param name="precision"></param>
        public ColumnType(ColumnValueType type, int length, int precision)
        {
            this.Type = type;
            this.Length = length;
            this.Precision = precision;
            CustomName = string.Empty;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="length"></param>
        public ColumnType(ColumnValueType type, int length)
        {
            this.Type = type;
            this.Length = length;
            this.Precision = 0;
            CustomName = string.Empty;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        /// <param name="type"></param>
        public ColumnType(ColumnValueType type)
        {
            this.Type = type;
            this.Length = 0;
            this.Precision = 0;
            CustomName = string.Empty;
        }
    }
}
