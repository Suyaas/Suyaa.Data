namespace Suyaa.Data.DbWorks.Dependency
{
    /// <summary>
    /// 工作者管理器供应商
    /// </summary>
    public interface IDbWorkManagerProvider
    {
        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <returns></returns>
        IDbWorkManager CreateManager();
    }
}
