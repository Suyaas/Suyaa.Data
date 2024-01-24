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
    /// 表达式编译器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExpressionCompiler<T>
        where T : Expression
    {
        // 私有变量
        private IDbScriptProvider? _provider;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="provider"></param>
        public void Initialize(IDbScriptProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 脚本供应商
        /// </summary>
        public IDbScriptProvider Provider => _provider ?? throw new DbException("Initialize", $"Please execute the Initialize() first.");

        /// <summary>
        /// 获取语句
        /// </summary>
        /// <returns></returns>
        public abstract string GetStatement(T expression);
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
                case EntityQueryRootExpression queryRootExpression: return StatementBuilder.Create(_provider, queryRootExpression).Build<EntityQueryRootCompiler>();
                default: throw new TypeNotSupportedException(expression.GetType());
            }
        }
    }
}
