using Suyaa.Data.Compilers.Builders;
using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        /// <summary>
        /// 获取表达式的值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="ExpressionNodeNotSupportedException"></exception>
        public ValueSet GetExpressionValue(Expression expression, QueryRootModel model)
        {
            switch (expression)
            {
                // 一元表达式
                case UnaryExpression unaryExpression:
                    return CreateStatementBuilder(unaryExpression).GetValue<UnaryCompiler>(model);
                // 二元表达式
                case BinaryExpression binaryExpression:
                    return ValueSet.Create(Sets.ValueType.Expression, "(" + CreateStatementBuilder(binaryExpression).GetValue<BinaryCompiler>(model).ToString() + ")");
                // 函数表达式
                case MethodCallExpression methodCallExpression:
                    return CreateStatementBuilder(methodCallExpression).GetValue<MethodCallCompiler>(model);
                // 成员变量
                case MemberExpression memberExpression:
                    return CreateStatementBuilder(memberExpression).GetValue<MemberCompiler>(model);
                // 常量
                case ConstantExpression constantExpression:
                    return CreateStatementBuilder(constantExpression).GetValue<ConstantCompiler>(model);
                // 建模参数
                case ParameterExpression _:
                    return ValueSet.Create(Sets.ValueType.Object, model);
                default: throw new ExpressionNodeNotSupportedException(expression.NodeType);
            }
        }

        /// <summary>
        /// 从对象字段或属性中获取内容
        /// </summary>
        /// <param name="valueSet"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected static object? GetFieldOrPropertyValue(ValueSet valueSet, string name)
        {
            if (valueSet.Value is null) return null;
            Type type = valueSet.Value.GetType();
            // 尝试从字段获取
            var field = type.GetField(name);
            if (field != null) return field.GetValue(valueSet.Value);
            // 尝试从属性获取
            var pro = type.GetProperty(name);
            if (pro != null) return pro.GetValue(valueSet.Value);
            return null;
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
