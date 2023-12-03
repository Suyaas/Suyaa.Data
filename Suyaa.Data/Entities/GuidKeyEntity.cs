using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Suyaa.Data.Entities
{
    /// <summary>
    /// 带主键的实例
    /// </summary>
    public class GuidKeyEntity : Entity<string>
    {

        /// <summary>
        /// GUID标识
        /// </summary>
        [Key]
        [StringLength(36)]
        public override string Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public GuidKeyEntity() : base(sy.Generator.GetNewNGuid()) { }

    }
}
