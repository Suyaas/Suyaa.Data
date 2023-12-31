﻿namespace Suyaa.Data.Entities
{
    /// <summary>
    /// 带雪花Id主键的实例
    /// </summary>
    public class SnowflakeKeyEntity : Entity<long>
    {

        /// <summary>
        /// 自动增长标识
        /// </summary>
        public override long Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// 对象实例化
        /// </summary>
        public SnowflakeKeyEntity() : base(sy.Generator.GetSnowflakeId()) { }

    }
}
