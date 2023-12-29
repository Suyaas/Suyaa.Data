using Suyaa.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Sqlite.Entities
{
    /// <summary>
    /// 版本信息
    /// </summary>
    [Table("version_info")]
    public class VersionInfo : CommonEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 版本
        /// </summary>
        [Column("version")]
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;
    }
}
