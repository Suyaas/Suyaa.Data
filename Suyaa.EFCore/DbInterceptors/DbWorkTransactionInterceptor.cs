using Microsoft.EntityFrameworkCore.Diagnostics;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.DbWorks.Helpers;
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
    public sealed class DbWorkTransactionInterceptor : DbTransactionInterceptor
    {
        private readonly IDbWork _work;

        /// <summary>
        /// 命令拦截器
        /// </summary>
        /// <param name="work"></param>
        public DbWorkTransactionInterceptor(IDbWork work)
        {
            _work = work;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult TransactionCommitting(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result)
        {
            return base.TransactionCommitting(transaction, eventData, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult> TransactionCommittingAsync(DbTransaction transaction, TransactionEventData eventData, InterceptionResult result, CancellationToken cancellationToken = default)
        {
            return base.TransactionCommittingAsync(transaction, eventData, result, cancellationToken);
        }
    }
}
