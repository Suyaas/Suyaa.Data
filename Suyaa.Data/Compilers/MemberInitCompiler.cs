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
    public sealed class MemberInitCompiler : ExpressionCompiler<MemberInitExpression>, IColumnsable<MemberInitExpression>
    {
        // 获取列信息
        private SelectColumnSet GetMemberAssignmentColumn(MemberAssignment memberAssignment, QueryRootModel model)
        {
            var member = memberAssignment.Member;
            var expression = memberAssignment.Expression;
            var type = expression.GetType();
            switch (expression)
            {
                case MemberExpression memberExpression:
                    var column = CreateStatementBuilder(memberExpression).GetColumn<MemberCompiler>(model);
                    return new SelectColumnSet(column.ColumnName, member.Name);
            }
            return new SelectColumnSet("", member.Name);
        }

        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<SelectColumnSet> GetColumns(MemberInitExpression expression, QueryRootModel model)
        {
            List<SelectColumnSet> columns = new List<SelectColumnSet>();
            var memberBindings = expression.Bindings;
            foreach (var memberBinding in memberBindings)
            {
                Type type = memberBinding.GetType();
                switch (memberBinding)
                {
                    case MemberAssignment memberAssignment:
                        //var pro = model.Columns.Where(d => d.PropertyInfo.Name == member.Name).FirstOrDefault();
                        //if (pro is null) throw new NotExistException(member.Name);
                        columns.Add(GetMemberAssignmentColumn(memberAssignment, model));
                        break;
                }
            }
            return columns;
        }
    }
}
