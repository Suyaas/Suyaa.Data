using Suyaa.Data.Attributes;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Suyaa.Data.Entities
{

    /// <summary>
    /// 带主键的实例
    /// </summary>
    public abstract class Entity<TId> : IDbEntity<TId> 
        where TId : notnull
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Key]
        public virtual TId Id { get; set; }

        /// <summary>
        /// 带主键的实例
        /// </summary>
        /// <param name="id"></param>
        public Entity(TId id)
        {
            Id = id;
        }

    }
}
