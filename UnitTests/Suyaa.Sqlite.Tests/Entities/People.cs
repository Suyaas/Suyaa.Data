using Suyaa.Data.Entities;
using Suyaa.Data.Kernel.Attributes;
using Suyaa.Data.Kernel.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Suyaa.Sqlite.Tests.Entities
{
    /// <summary>
    /// 人员
    /// </summary>
    [Table("people")]
    public class People : GuidKeyEntity
    {

        /// <summary>
        /// 名称
        /// </summary>
        [Column]
        [ColumnType(ColumnValueType.Varchar, 128)]
        [StringLength(128)]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 年龄
        /// </summary>
        [Column]
        public virtual int Age { get; set; } = 0;

        /// <summary>
        /// 部门Id
        /// </summary>
        [Column]
        [ColumnType(ColumnValueType.Varchar, 50)]
        [StringLength(50)]
        public virtual string DepartmentId { get; set; } = string.Empty;
    }
}
