using Suyaa.Data.Expressions.Compilers;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Expressions
{
    /// <summary>
    /// 表达式编译器构建器
    /// </summary>
    public sealed class StatementBuilder
    {
        /// <summary>
        /// 创建表达式编译器构建器
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static StatementBuilder<TExpression> Create<TExpression>(IDbScriptProvider provider, TExpression expression)
            where TExpression : Expression
            => new StatementBuilder<TExpression>(provider, expression);
    }

    /// <summary>
    /// 表达式编译器构建器
    /// </summary>
    public sealed class StatementBuilder<TExpression>
        where TExpression : Expression
    {
        private readonly IDbScriptProvider _provider;
        private readonly TExpression _expression;

        /// <summary>
        /// 表达式编译器构建器
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        public StatementBuilder(IDbScriptProvider provider, TExpression expression)
        {
            _provider = provider;
            _expression = expression;
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <typeparam name="TCompiler"></typeparam>
        /// <returns></returns>
        public string Build<TCompiler>()
            where TCompiler : ExpressionCompiler<TExpression>, new()
        {
            TCompiler compiler = new TCompiler();
            compiler.Initialize(_provider);
            return compiler.GetStatement(_expression);
        }
    }
}
