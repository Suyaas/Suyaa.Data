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
    public sealed class NewCompiler : ExpressionCompiler<NewExpression>, IColumnsable<NewExpression>
    {
        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<SelectColumnSet> GetColumns(NewExpression expression, QueryRootModel model)
        {
            List<SelectColumnSet> columns = new List<SelectColumnSet>();
            foreach (var member in expression.Members)
            {

            }
            //columns.Add(ConvertMemberToString(member, entity.Columns));
            return columns;
        }
    }
}
