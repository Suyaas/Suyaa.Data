using System.ComponentModel.DataAnnotations.Schema;

namespace Suyaa.Data.Entities
{
    /// <summary>
    /// 带自增长主键的实例
    /// </summary>
    public class AutoIncrementKeyEntity : Entity<long>
    {

        /// <summary>
        /// 自动增长标识
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override long Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// 带自增长主键的实例
        /// </summary>
        public AutoIncrementKeyEntity() : base(0) { }

    }
}
