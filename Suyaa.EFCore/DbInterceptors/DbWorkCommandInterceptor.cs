using Microsoft.EntityFrameworkCore.Diagnostics;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Suyaa.EFCore.DbInterceptors
{
    /// <summary>
    /// 命令拦截器
    /// </summary>
    public sealed class DbWorkCommandInterceptor : DbCommandInterceptor
    {
        private readonly IDbWork _work;
        private readonly ISqlRepository _sqlRepository;

        /// <summary>
        /// 命令拦截器
        /// </summary>
        /// <param name="work"></param>
        public DbWorkCommandInterceptor(IDbWork work)
        {
            _work = work;
            _sqlRepository = _work.GetSqlRepository();
        }

        //public override int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
        //{
        //    return base.NonQueryExecuted(command, eventData, result);
        //}

        //public override ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result, CancellationToken cancellationToken = default)
        //{
        //    return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
        //}

        //public override void CommandCanceled(DbCommand command, CommandEndEventData eventData)
        //{
        //    base.CommandCanceled(command, eventData);
        //}

        //public override Task CommandCanceledAsync(DbCommand command, CommandEndEventData eventData, CancellationToken cancellationToken = default)
        //{
        //    return base.CommandCanceledAsync(command, eventData, cancellationToken);
        //}

        //public override DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
        //{
        //    return base.CommandCreated(eventData, result);
        //}

        /// <summary>
        /// 命令管理器创建
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
        {
            return InterceptionResult<DbCommand>.SuppressWithResult(_sqlRepository.GetDbCommand());
            //return base.CommandCreating(eventData, result);
        }

        //public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
        //{
        //    base.CommandFailed(command, eventData);
        //}

        //public override Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData, CancellationToken cancellationToken = default)
        //{
        //    return base.CommandFailedAsync(command, eventData, cancellationToken);
        //}

        /// <summary>
        /// 数据库命令初始化
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override DbCommand CommandInitialized(CommandEndEventData eventData, DbCommand result)
        {
            // 过滤
            if (eventData.CommandSource == CommandSource.SaveChanges)
            {
                result.Transaction = _work.Transaction;
            }
            return result;
            //return base.CommandInitialized(eventData, result);
        }

        //public override InterceptionResult DataReaderClosing(DbCommand command, DataReaderClosingEventData eventData, InterceptionResult result)
        //{
        //    return base.DataReaderClosing(command, eventData, result);
        //}

        //public override ValueTask<InterceptionResult> DataReaderClosingAsync(DbCommand command, DataReaderClosingEventData eventData, InterceptionResult result)
        //{
        //    return base.DataReaderClosingAsync(command, eventData, result);
        //}

        //public override InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData, InterceptionResult result)
        //{
        //    return base.DataReaderDisposing(command, eventData, result);
        //}

        //public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        //{
        //    return base.NonQueryExecuting(command, eventData, result);
        //}

        //public override ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        //{
        //    return base.NonQueryExecutingAsync(command, eventData, result, cancellationToken);
        //}

        //public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        //{
        //    return base.ReaderExecuted(command, eventData, result);
        //}

        //public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        //{
        //    return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        //}

        /// <summary>
        /// 命令执行时
        /// </summary>
        /// <param name="command"></param>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command = _work.DbCommandExecuting(command);
            return base.ReaderExecuting(command, eventData, result);
        }

        //public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        //{
        //    return base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
        //}

        //public override object? ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object? result)
        //{
        //    return base.ScalarExecuted(command, eventData, result);
        //}

        //public override ValueTask<object?> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object? result, CancellationToken cancellationToken = default)
        //{
        //    return base.ScalarExecutedAsync(command, eventData, result, cancellationToken);
        //}

        //public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        //{
        //    return base.ScalarExecuting(command, eventData, result);
        //}

        //public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        //{
        //    return base.ScalarExecutingAsync(command, eventData, result, cancellationToken);
        //}
    }
}
