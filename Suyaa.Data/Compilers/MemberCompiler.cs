using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Entities;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 函数执行
    /// </summary>
    public sealed class MemberCompiler : ExpressionCompiler<MemberExpression>, IColumnable<MemberExpression>
    {
        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public SelectColumnSet GetColumn(MemberExpression expression, DbEntityModel model)
        {
            var member = expression.Member;
            var pro = model.Columns.Where(d => d.PropertyInfo.Name == member.Name).FirstOrDefault();
            if (pro is null) throw new NotExistException(member.Name);
            return new SelectColumnSet(pro.Name, pro.PropertyInfo.Name);
        }
    }
}
