using Suyaa.Data.Entities;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据库供应商助手
    /// </summary>
    public static class DbScriptProviderHelper
    {
        /// <summary>
        /// DbValue
        /// </summary>
        public static string Name_DBValue = typeof(DbValue).FullName;

        /// <summary>
        /// 获取实例Insert脚本
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetEntityInsert(this IDbScriptProvider provider, DbEntityModel entity)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();
            foreach (var column in entity.Columns)
            {
                if (column.IsAutoIncrement) continue;
                if (columns.Length > 0) columns.Append(',');
                columns.Append(provider.GetName(column.Name));
                if (values.Length > 0) values.Append(',');
                values.Append(provider.GetVariable("V_" + column.Index));
            }
            // 拼接sql脚本
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            if (!entity.Schema.IsNullOrWhiteSpace())
            {
                sql.Append(entity.Schema);
                sql.Append('.');
            }
            sql.Append(provider.GetName(entity.Name));
            sql.Append(" (");
            sql.Append(columns.ToString());
            sql.Append(") VALUES (");
            sql.Append(values.ToString());
            sql.Append(")");
            return sql.ToString();
        }

        /// <summary>
        /// 获取实例Delete脚本
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static string GetEntityDelete<TEntity>(this IDbScriptProvider provider, DbEntityModel entity, Expression<Func<TEntity, bool>> predicate)
        {
            // 拼接sql脚本
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM ");
            if (!entity.Schema.IsNullOrWhiteSpace())
            {
                sql.Append(entity.Schema);
                sql.Append('.');
            }
            sql.Append(provider.GetName(entity.Name));
            sql.Append(" WHERE 1=1");
            var predicateString = provider.GetPredicate(entity, predicate);
            if (!predicateString.IsNullOrWhiteSpace())
            {
                sql.Append(" AND ");
                sql.Append(predicateString);
            }
            return sql.ToString();
        }

        /// <summary>
        /// 获取实例Update脚本
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static string GetEntityUpdate<TEntity>(this IDbScriptProvider provider, DbEntityModel entity, IEnumerable<ColumnModel> fields, Expression<Func<TEntity, bool>> predicate)
        {
            StringBuilder columns = new StringBuilder();
            foreach (var field in fields)
            {
                if (field.IsAutoIncrement) continue;
                if (columns.Length > 0) columns.Append(',');
                columns.Append(provider.GetName(field.Name));
                columns.Append(" = ");
                columns.Append(provider.GetVariable("V_" + field.Index));
            }
            // 拼接sql脚本
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE ");
            if (!entity.Schema.IsNullOrWhiteSpace())
            {
                sql.Append(entity.Schema);
                sql.Append('.');
            }
            sql.Append(provider.GetName(entity.Name));
            sql.Append(" SET ");
            sql.Append(columns.ToString());
            sql.Append(" WHERE 1=1");
            var predicateString = provider.GetPredicate(entity, predicate);
            if (!predicateString.IsNullOrWhiteSpace())
            {
                sql.Append(" AND ");
                sql.Append(predicateString);
            }
            return sql.ToString();
        }

        #region 处理函数表达式

        /// <summary>
        /// 获取Contains函数兼容的sql语句
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetContainsMethodScript(this IDbScriptProvider provider, DbEntityModel entity, MethodCallExpression exp)
        {
            var callObj = (MemberExpression)exp.Object;
            var callObjValues = provider.GetExpressionValue(entity, callObj.Expression) ?? throw new NullException("MemberExpression.Object");
            var listInfo = callObjValues.GetType().GetField(callObj.Member.Name);
            ICollection list = (ICollection)listInfo.GetValue(callObjValues);
            StringBuilder sbList = new StringBuilder();
            sbList.Append('(');
            foreach (var item in list)
            {
                if (sbList.Length > 1) sbList.Append(", ");
                if (item is null) { sbList.Append("NULL"); continue; }
                if (item.GetType().IsNumeric()) { sbList.Append(item); continue; }
                if (item is string str) { sbList.Append(provider.GetStringValue(str)); continue; }
                throw new TypeNotSupportedException(item.GetType());
            }
            sbList.Append(')');
            var arg = provider.GetExpressionValue(entity, exp.Arguments[0]);
            return $"{arg} IN {sbList}";
        }

        /// <summary>
        /// 获取Equals函数兼容的sql语句
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetEqualsMethodScrip(this IDbScriptProvider provider, DbEntityModel entity, MethodCallExpression exp)
        {
            var callObj = (MemberExpression)exp.Object;
            object? value;
            string name;
            switch (callObj.Expression)
            {
                case ParameterExpression _:
                    name = provider.GetName(callObj.Member.Name);
                    value = provider.GetExpressionValue(entity, exp.Arguments[0]);
                    break;
                default:
                    value = provider.GetExpressionValue(entity, callObj.Expression);
                    name = exp.Arguments[0].NodeType.ToString();
                    break;
            }
            if (value is null) return $"{name} IS NULL";
            return $"{name} = {value}";
        }

        /// <summary>
        /// 获取单参数DbValue函数脚本
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <param name="scriptCreate"></param>
        public static string GetSingleArgumentDbValueMethodScrip(this IDbScriptProvider provider, DbEntityModel entity, MethodCallExpression exp, Func<string, string> scriptCreate)
        {
            if (exp.Arguments.Count != 1) throw new ExpressionNodeNonStandardException($"Call.IsNUll({exp.Arguments.Count})");
            // 获取需要判断的主体
            var arg = exp.Arguments[0];
            if (!(arg is UnaryExpression argUnary)) throw new ExpressionNodeNonStandardException($"Call.IsNUll({arg.NodeType})");
            if (argUnary.NodeType != ExpressionType.Convert) throw new ExpressionNodeNonStandardException($"Call.IsNUll({arg.NodeType})");
            // 定义名称
            string name;
            // 判断主体类型
            switch (argUnary.Operand)
            {
                case MemberExpression member:
                    // 获取对象表达式
                    if (!(member.Expression is ParameterExpression parameter)) throw new ExpressionNodeNonStandardException($"Call.IsNUll({member.NodeType})");
                    // 判断是否为传入对象
                    if (parameter.Type != entity.Type) throw new ExpressionNodeNonStandardException($"Call.IsNUll({parameter.Type.FullName})");
                    // 获取字段名称
                    var fieldName = entity.GetFieldName(member.Member.Name);
                    if (fieldName.IsNullOrWhiteSpace()) throw new NotExistException($"{entity.Type.Name}.{member.Member.Name}");
                    name = provider.GetName(fieldName);
                    break;
                default:
                    throw new ExpressionNodeNotSupportedException($"Call.IsNUll({argUnary.Operand.NodeType})");
            }
            return scriptCreate(name);
        }

        /// <summary>
        /// 获取Equals函数兼容的sql语句
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetIsNullMethodScrip(this IDbScriptProvider provider, DbEntityModel entity, MethodCallExpression exp)
        {
            return provider.GetSingleArgumentDbValueMethodScrip(entity, exp, name => $"{name} IS NULL");
        }

        /// <summary>
        /// 获取Equals函数兼容的sql语句
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetIsNotNullMethodScrip(this IDbScriptProvider provider, DbEntityModel entity, MethodCallExpression exp)
        {
            return provider.GetSingleArgumentDbValueMethodScrip(entity, exp, name => $"{name} IS NOT NULL");
        }

        #endregion

        #region 处理查询表达式

        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this IDbScriptProvider provider, string value)
        {
            return "'" + value.Replace("'", "''") + "'";
        }

        /// <summary>
        /// 获取脚本字符串值
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetScriptValue(this IDbScriptProvider provider, object? value)
        {
            if (value is null) return "NULL";
            if (value is string str) return provider.GetStringValue(str);
            return Convert.ToString(value);
        }

        // 从对象字段或属性中获取内容
        private static object? GetFieldOrPropertyValue(object obj, string name)
        {
            Type type = obj.GetType();
            // 尝试从字段获取
            var field = type.GetField(name);
            if (field != null) return field.GetValue(obj);
            // 尝试从属性获取
            var pro = type.GetProperty(name);
            if (pro != null) return pro.GetValue(obj);
            return null;
        }

        /// <summary>
        /// 获取函数调用处理sql语句
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        /// <exception cref="ExpressionNodeNotSupportedException"></exception>
        public static string GetMethodCallExpressionScript(this IDbScriptProvider provider, DbEntityModel entity, MethodCallExpression exp)
        {
            var callMethod = exp.Method;
            // 扩展值函数支持
            if (callMethod.DeclaringType.FullName == Name_DBValue)
            {
                switch (callMethod.Name)
                {
                    case nameof(DbValue.IsNull):
                        return provider.GetIsNullMethodScrip(entity, exp);
                    case nameof(DbValue.IsNotNull):
                        return provider.GetIsNotNullMethodScrip(entity, exp);

                }
            }
            return callMethod.Name switch
            {
                "Contains" => provider.GetContainsMethodScript(entity, exp),
                "Equals" => provider.GetEqualsMethodScrip(entity, exp),
                _ => throw new ExpressionNodeNotSupportedException($"Call.{callMethod.Name}"),
            };
        }

        /// <summary>
        /// 获取一元表达式的值
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetUnaryExpressionScript(this IDbScriptProvider provider, DbEntityModel entity, UnaryExpression exp)
        {
            object? value = null;
            if (exp.Operand is UnaryExpression unaryExpression) value = provider.GetUnaryExpressionScript(entity, unaryExpression);
            if (exp.Operand is ConstantExpression constantExpression) value = provider.GetExpressionValue(entity, constantExpression);
            if (value is null)
            {
                var operand = (MemberExpression)exp.Operand;
                var operandValues = provider.GetExpressionValue(entity, operand.Expression) ?? throw new NullException("MemberExpression.Object");
                value = GetFieldOrPropertyValue(operandValues, operand.Member.Name);
            }
            return value switch
            {
                null => "NULL",
                string _ => provider.GetStringValue((string)value),
                _ => Convert.ToString(value),
            };
        }

        /// <summary>
        /// 获取表达式的值
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        /// <exception cref="NullException"></exception>
        /// <exception cref="ExpressionNodeNotSupportedException"></exception>
        public static object? GetExpressionValue(this IDbScriptProvider provider, DbEntityModel entity, Expression exp)
        {
            switch (exp)
            {
                // 一元表达式
                case UnaryExpression unaryExpression: return provider.GetUnaryExpressionScript(entity, unaryExpression);
                // 二元表达式
                case BinaryExpression binaryExpression:
                    return "(" + provider.GetBinaryExpressionScript(entity, binaryExpression) + ")";
                // 函数表达式
                case MethodCallExpression methodCallExpression:
                    return provider.GetMethodCallExpressionScript(entity, methodCallExpression);
                // 变量
                case MemberExpression memberExpression:
                    if (memberExpression.Expression is ParameterExpression parameterExpression)
                    {
                        // 查询实例属性
                        var column = entity.Columns.Where(d => d.PropertyInfo.Name == memberExpression.Member.Name).FirstOrDefault();
                        if (column is null) throw new NotExistException(memberExpression.Member.Name);
                        return provider.GetName(column.Name);
                    }
                    // 变量名称
                    var memberName = memberExpression.Member.Name;
                    // 变量所属对象
                    var memberObject = provider.GetExpressionValue(entity, memberExpression.Expression) ?? throw new NullException(typeof(ConstantExpression));
                    // 变量值
                    var memberValue = GetFieldOrPropertyValue(memberObject, memberName);
                    // 变量值类型
                    var memberValueType = memberValue?.GetType();
                    if (memberValueType.IsNumeric()) return memberValue;
                    if (memberValue is string memberStringValue) return provider.GetStringValue(memberStringValue);
                    return memberValue;
                //switch (member.Expression)
                //{
                //    // 如果包含表达式，则先解析表达式
                //    case ConstantExpression constant:
                //        var parent = provider.GetExpressionValue(entity, constant) ?? throw new NullException(typeof(ConstantExpression));
                //        if(parent is string strParent) return strParent;
                //        return provider.GetScriptValue(GetFieldOrPropertyValue(parent, member.Member.Name));
                //    default:
                //        // 查询实例属性
                //        var field = entity.Fields.Where(d => d.PropertyInfo.Name == member.Member.Name).FirstOrDefault();
                //        if (field is null) throw new NotExistException(member.Member.Name);
                //        return provider.GetName(field.Name);
                //}
                // 常量
                case ConstantExpression constantExpression:
                    if (constantExpression.Value is null) return "NULL";
                    if (constantExpression.Value is string str) return provider.GetStringValue(str);
                    var valueType = constantExpression.Value.GetType();
                    if (valueType.IsNumeric()) return constantExpression.Value.ToString();
                    return constantExpression.Value;
                default: throw new ExpressionNodeNotSupportedException(exp.NodeType);
            }
        }

        /// <summary>
        /// 获取二元表达式脚本
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static string GetBinaryExpressionScript(this IDbScriptProvider provider, DbEntityModel entity, BinaryExpression exp)
        {
            StringBuilder sb = new StringBuilder();
            string expLeft = Convert.ToString(provider.GetExpressionValue(entity, exp.Left) ?? "");
            string expRight = Convert.ToString(provider.GetExpressionValue(entity, exp.Right) ?? "");
            sb.Append(expLeft);
            switch (exp.NodeType)
            {
                case ExpressionType.AndAlso:
                    sb.Append(" AND ");
                    break;
                case ExpressionType.OrElse:
                    sb.Append(" OR ");
                    break;
                case ExpressionType.Equal:
                    if (expRight == "NULL")
                    {
                        sb.Append(" IS ");
                    }
                    else
                    {
                        sb.Append(" = ");
                    }
                    break;
                case ExpressionType.NotEqual:
                    if (expRight == "NULL")
                    {
                        sb.Append(" IS NOT ");
                    }
                    else
                    {
                        sb.Append(" <> ");
                    }
                    break;
                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;
                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;
                case ExpressionType.Coalesce:
                    return $"COALESCE({expLeft}, {expRight})";
                default: throw new ExpressionNodeNotSupportedException(exp.NodeType);
            }
            sb.Append(expRight);
            return sb.ToString();
        }

        /// <summary>
        /// 获取查询脚本
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static string GetPredicate<TEntity>(this IDbScriptProvider provider, DbEntityModel entity, Expression<Func<TEntity, bool>> predicate)
        {
            return predicate.Body switch
            {
                BinaryExpression binaryExpression => provider.GetBinaryExpressionScript(entity, binaryExpression),
                MethodCallExpression methodCallExpression => provider.GetMethodCallExpressionScript(entity, methodCallExpression),
                _ => throw new ExpressionNodeNotSupportedException(predicate.Body.NodeType),
            };
        }

        #endregion
    }
}
