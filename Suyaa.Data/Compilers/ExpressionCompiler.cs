using Suyaa.Data.Compilers.Builders;
using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 表达式编译器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExpressionCompiler<T>
        where T : Expression
    {
        // 私有变量
        private IDbScriptProvider? _provider;
        private VariableBuilder? _variableBuilder;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="provider"></param>
        public void Initialize(IDbScriptProvider provider)
        {
            _provider = provider;
            _variableBuilder = new VariableBuilder();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="variableBuilder"></param>
        public void Initialize(IDbScriptProvider provider, VariableBuilder variableBuilder)
        {
            _provider = provider;
            _variableBuilder = variableBuilder;
        }

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider Provider => _provider ?? throw new DbException("Initialize", $"Please execute the Initialize() first.");

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public VariableBuilder VariableBuilder => _variableBuilder ?? throw new DbException("Initialize", $"Please execute the Initialize() first.");

        /// <summary>
        /// 创建语句构建器
        /// </summary>
        /// <typeparam name="TExpression"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected StatementBuilder<TExpression> CreateStatementBuilder<TExpression>(TExpression expression)
            where TExpression : Expression
        {
            return StatementBuilder.Create(Provider, expression).SetVariableBuilder(VariableBuilder);
        }
    }

    /// <summary>
    /// 表达式编译器
    /// </summary>
    public sealed class ExpressionCompiler
    {
        private readonly IDbScriptProvider _provider;

        /// <summary>
        /// 表达式编译器
        /// </summary>
        /// <param name="provider"></param>
        public ExpressionCompiler(IDbScriptProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 创建语句构建器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public StatementBuilder<T> CreateStatementBuilder<T>(T expression)
            where T : Expression
        {
            return StatementBuilder.Create(_provider, expression);
        }

        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="TypeNotSupportedException"></exception>
        public string GetStatement(Expression expression)
        {
            switch (expression)
            {
                // 实体根查询表达式
                case QueryRootExpression queryRootExpression: return CreateStatementBuilder(queryRootExpression).GetStatement<QueryRootCompiler>();
                // 函数执行
                case SelectExpression selectExpression: return CreateStatementBuilder(selectExpression).GetStatement<SelectCompiler>();
                // 未支持
                default: throw new TypeNotSupportedException(expression.GetType());
            }
        }
    }
}
