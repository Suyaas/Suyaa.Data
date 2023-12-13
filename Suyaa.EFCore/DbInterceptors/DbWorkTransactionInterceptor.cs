using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.EFCore.DbInterceptors
{
    /// <summary>
    /// 数据库事务拦截器
    /// </summary>
    public sealed class DbWorkTransactionInterceptor : DbTransactionInterceptor
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult TransactionCommitting(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
        {
            return InterceptionResult.Suppress();
            //return base.TransactionCommitting(transaction, eventData, result);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async ValueTask<InterceptionResult> TransactionCommittingAsync(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(InterceptionResult.Suppress());
            //return base.TransactionCommittingAsync(transaction, eventData, result, cancellationToken);
        }
    }
}
