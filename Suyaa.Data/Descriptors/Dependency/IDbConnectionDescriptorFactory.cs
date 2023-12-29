namespace Suyaa.Data.Descriptors.Dependency
{
    /// <summary>
    /// 主机数据库上下文配置工厂
    /// </summary>
    public interface IDbConnectionDescriptorFactory
    {
        /// <summary>
        /// 获取主机数据库上下文配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDbConnectionDescriptor GetConnection(string name);

        /// <summary>
        /// 默认主机数据库上下文配置
        /// </summary>
        IDbConnectionDescriptor DefaultConnection { get; }
    }
}
