using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Oracle.Maintenances.Dto
{
    /// <summary>
    /// 字段信息
    /// </summary>
    [Table("DBA_TAB_COLUMNS")]
    public sealed class ColumnInfo
    {
        /// <summary>
        /// 字段Id
        /// </summary>
        [Column("COLUMN_ID")]
        public long ColomnId { get; set; } = 0;
        /// <summary>
        /// 字段名称
        /// </summary>
        [Column("COLUMN_NAME")]
        public string ColomnName { get; set; } = string.Empty;
        /// <summary>
        /// 数据类型
        /// </summary>
        [Column("DATA_TYPE")]
        public string DataType { get; set; } = string.Empty;
        /// <summary>
        /// 数据长度
        /// </summary>
        [Column("DATA_LENGTH")]
        public int DataLength { get; set; } = 0;
        /// <summary>
        /// 数据精度
        /// </summary>
        [Column("DATA_PRECISION")]
        public int? DataPrecision { get; set; } = 0;
        /// <summary>
        /// 可为空
        /// </summary>
        [Column("NULLABLE")]
        public char Nullable { get; set; } = '\0';
    }
}
