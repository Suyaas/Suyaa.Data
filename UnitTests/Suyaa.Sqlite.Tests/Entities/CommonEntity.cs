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
    /// 
    /// </summary>
    public class CommonEntity : UUIDKeyEntity
    {
        /// <summary>
        /// 唯一Id
        /// </summary>
        [Column("id")]
        public override string Id { get => base.Id; set => base.Id = value; }
    }
}
