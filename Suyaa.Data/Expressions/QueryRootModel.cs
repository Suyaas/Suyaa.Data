using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Expressions
{
    /// <summary>
    /// 根查询建模信息
    /// </summary>
    public sealed class QueryRootModel
    {
        /// <summary>
        /// 根查询建模信息
        /// </summary>
        public QueryRootModel(DbEntityModel model, string alias)
        {
            EntityModel = model;
            Alias = alias;
        }

        /// <summary>
        /// 实体建模
        /// </summary>
        public DbEntityModel EntityModel { get; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; }
    }
}
