using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Suyaa.Data.Compilers.Builders
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
        private VariableBuilder? _variableBuilder;

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
        /// 设置变量构建器
        /// </summary>
        /// <param name="variableBuilder"></param>
        /// <returns></returns>
        public StatementBuilder<TExpression> SetVariableBuilder(VariableBuilder variableBuilder)
        {
            _variableBuilder = variableBuilder;
            return this;
        }

        // 编译器初始化
        private void CompilerInitialize<TCompiler>(TCompiler compiler)
            where TCompiler : ExpressionCompiler<TExpression>
        {
            if (_variableBuilder is null)
            {
                compiler.Initialize(_provider);
            }
            else
            {
                compiler.Initialize(_provider, _variableBuilder);
            }
        }

        /// <summary>
        /// 获取语句
        /// </summary>
        /// <typeparam name="TCompiler"></typeparam>
        /// <returns></returns>
        public string GetStatement<TCompiler>()
            where TCompiler : ExpressionCompiler<TExpression>, IStatementable<TExpression>, new()
        {
            TCompiler compiler = new TCompiler();
            CompilerInitialize(compiler);
            return compiler.GetStatement(_expression);
        }

        /// <summary>
        /// 获取From语句
        /// </summary>
        /// <typeparam name="TCompiler"></typeparam>
        /// <returns></returns>
        public string GetFromStatement<TCompiler>()
            where TCompiler : ExpressionCompiler<TExpression>, IFromable<TExpression>, new()
        {
            TCompiler compiler = new TCompiler();
            CompilerInitialize(compiler);
            return compiler.GetFromStatement(_expression);
        }

        /// <summary>
        /// 获取查询列集合
        /// </summary>
        /// <typeparam name="TCompiler"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<SelectColumnSet> GetColumns<TCompiler>(QueryRootModel model)
            where TCompiler : ExpressionCompiler<TExpression>, IColumnsable<TExpression>, new()
        {
            TCompiler compiler = new TCompiler();
            CompilerInitialize(compiler);
            return compiler.GetColumns(_expression, model);
        }

        /// <summary>
        /// 获取查询列
        /// </summary>
        /// <typeparam name="TCompiler"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public SelectColumnSet GetColumn<TCompiler>(QueryRootModel model)
            where TCompiler : ExpressionCompiler<TExpression>, IColumnable<TExpression>, new()
        {
            TCompiler compiler = new TCompiler();
            CompilerInitialize(compiler);
            return compiler.GetColumn(_expression, model);
        }

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <typeparam name="TCompiler"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetPredicate<TCompiler>(QueryRootModel model)
            where TCompiler : ExpressionCompiler<TExpression>, IPredicatable<TExpression>, new()
        {
            TCompiler compiler = new TCompiler();
            CompilerInitialize(compiler);
            return compiler.GetPredicate(_expression, model);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="TCompiler"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueSet GetValue<TCompiler>(QueryRootModel model)
            where TCompiler : ExpressionCompiler<TExpression>, IValuable<TExpression>, new()
        {
            TCompiler compiler = new TCompiler();
            CompilerInitialize(compiler);
            return compiler.GetValue(_expression, model);
        }
    }
}
