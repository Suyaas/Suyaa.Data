using Microsoft.EntityFrameworkCore;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Models;
using System.Reflection;

namespace Suyaa.EFCore.Factories
{
    /// <summary>
    /// 数据库上下文工厂
    /// </summary>
    public class DbSetFactory : IDbSetFactory
    {

        private readonly List<DbSetModel> _dbSets;
        private readonly IEnumerable<IDbSetProvider> _providers;

        /// <summary>
        /// 数据库上下文工厂
        /// </summary>
        public DbSetFactory(
            IEnumerable<IDbSetProvider> providers
            )
        {
            _dbSets = new List<DbSetModel>();
            _providers = providers;
            // 添加所有 Dbset 描述
            foreach (var provider in _providers)
            {
                var dbSets = provider.GetDbSets();
                foreach (var dbSet in dbSets)
                {
                    if (_dbSets.Where(d => d.Type == dbSet.Type).Any()) continue;
                    _dbSets.Add(dbSet);
                }
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DbSetModel? GetDbSet(Type type)
        {
            return _dbSets.Where(d => d.Type == type).FirstOrDefault();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public DbSetModel? GetDbSet<TEntity>()
        {
            return GetDbSet(typeof(TEntity));
        }

        /// <summary>
        /// 获取连接中的所有数据实例
        /// </summary>
        /// <param name="dbConnectionDescriptorName"></param>
        /// <returns></returns>
        public IEnumerable<DbSetModel> GetDbSets(string dbConnectionDescriptorName)
        {
            return _dbSets.Where(d => d.ConnectionDescriptor.Name == dbConnectionDescriptorName).ToList();
        }
    }
}
