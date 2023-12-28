using Suyaa.Data.Enums;
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
        /// 占用大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public int Float { get; set; }
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
            this.Size = 0;
            this.Float = 0;
            CustomName = customName;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="flt"></param>
        public ColumnType(ColumnValueType type, int size, int flt)
        {
            this.Type = type;
            this.Size = size;
            this.Float = flt;
            CustomName = string.Empty;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        public ColumnType(ColumnValueType type, int size)
        {
            this.Type = type;
            this.Size = size;
            this.Float = 0;
            CustomName = string.Empty;
        }

        /// <summary>
        /// 列类型
        /// </summary>
        /// <param name="type"></param>
        public ColumnType(ColumnValueType type)
        {
            this.Type = type;
            this.Size = 0;
            this.Float = 0;
            CustomName = string.Empty;
        }
    }
}
