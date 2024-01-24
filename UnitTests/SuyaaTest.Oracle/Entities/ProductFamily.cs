using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.Oracle.Entities
{
    /// <summary>
    /// 机型
    /// </summary>
    [Table("PRODUCTFAMILY")]
    public sealed class ProductFamily : IDbEntity
    {
        /// <summary>
        /// 机型名称
        /// </summary>
        [Column("PRODUCTFAMILYNAME")]
        public string ProductFamilyName { get; set; } = string.Empty;
    }
}
