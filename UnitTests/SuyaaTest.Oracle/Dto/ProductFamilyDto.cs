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
    public sealed class ProductFamilyDto
    {
        /// <summary>
        /// 机型名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
