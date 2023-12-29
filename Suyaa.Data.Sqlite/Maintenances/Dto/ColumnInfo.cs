using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Sqlite.Maintenances.Dto
{
    /// <summary>
    /// 表信息
    /// </summary>
    [Table("table_info")]
    public sealed class ColumnInfo
    {
        /// <summary>
        /// cid
        /// </summary>
        [Column("cid")]
        public long Cid { get; set; } = 0;
        /// <summary>
        /// 字段名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 字段类型
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = string.Empty;
        /// <summary>
        /// 不可为空
        /// </summary>
        [Column("notnull")]
        public int NotNull { get; set; } = 0;
        /// <summary>
        /// 主键
        /// </summary>
        [Column("pk")]
        public int Pk { get; set; } = 0;
    }
}
