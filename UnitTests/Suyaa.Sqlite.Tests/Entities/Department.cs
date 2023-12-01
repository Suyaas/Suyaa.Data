using Suyaa.Data.Attributes;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using SuyaaTest.Sqlite.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Sqlite.Tests.Entities
{
    /// <summary>
    /// 部门
    /// </summary>
    [DbTable(Convert = NameConvertType.UnderlineLower)]
    public class Department : GuidKeyEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DbColumn(Convert = NameConvertType.UnderlineLower)]
        [DbColumnType(DatabaseColumnType.Varchar, 128)]
        [StringLength(128)]
        public virtual string Name { get; set; } = string.Empty;
    }
}
