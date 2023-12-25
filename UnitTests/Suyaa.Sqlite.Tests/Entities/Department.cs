using Suyaa.Data.Attributes;
using Suyaa.Data.Entities;
using Suyaa.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suyaa.Sqlite.Tests.Entities
{
    /// <summary>
    /// 部门
    /// </summary>
    [Table("department")]
    public class Department : GuidKeyEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Column]
        [ColumnType(ColumnValueType.Varchar, 128)]
        [StringLength(128)]
        public virtual string Name { get; set; } = string.Empty;
    }
}
