using Suyaa.EFCore.Models;

namespace Suyaa.EFCore.Dependency
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public interface IDbSetFactory
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        DbSetModel? GetDbSet(Type type);

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        DbSetModel? GetDbSet<TEntity>();

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="dbConnectionDescriptorName"></param>
        /// <returns></returns>
        IEnumerable<DbSetModel> GetDbSets(string dbConnectionDescriptorName);
    }
}
