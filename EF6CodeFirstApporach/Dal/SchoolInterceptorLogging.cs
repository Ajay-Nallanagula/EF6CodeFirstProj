using EF6CodeFirstApporach.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EF6CodeFirstApporach.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public class SchoolInterceptorLogging : DbCommandInterceptor
    {
        private readonly ILogger logger = new SchoolLogger();
        private readonly Stopwatch stopWatch = new Stopwatch();

        //public SchoolInterceptorLogging(ILogger logger)
        //{
        //    this.logger = logger;
        //    this.stopWatch = new Stopwatch();
        //}

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            stopWatch.Stop();
            if (interceptionContext.Exception!=null)
            {
                logger.Error(interceptionContext.Exception, $"Execption on executing SQL command \\n: {command.CommandText}",$"Elapsed Time {stopWatch.Elapsed.ToString()}");
            }
            else
            {
                logger.TraceApi("SQL Database", "SchoolInterceptorLogging\\NonQueryExecuted", stopWatch.Elapsed, $"Command Exceuted Successfully: {command.CommandText}");
            }
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            stopWatch.Restart();
            base.NonQueryExecuting(command, interceptionContext);
        }
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            stopWatch.Stop();
            if (interceptionContext.Exception!=null)
            {
                logger.Error(interceptionContext.Exception, $"Execption on executing SQL command \\n: {command.CommandText}", $"Elapsed Time {stopWatch.Elapsed.ToString()}");
            }
            else
            {
                logger.TraceApi("SQL Database", "SchoolInterceptorLogging\\ReaderExecuted", stopWatch.Elapsed, $"Command Exceuted Successfully: {command.CommandText}");
            }
            base.ReaderExecuted(command, interceptionContext);
        }
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            stopWatch.Restart();
            base.ReaderExecuting(command, interceptionContext);
        }
        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            stopWatch.Stop();
            if (interceptionContext.Exception != null)
            {
                logger.Error(interceptionContext.Exception, $"Execption on executing SQL command \\n: {command.CommandText}", $"Elapsed Time {stopWatch.Elapsed.ToString()}");
            }
            else
            {
                logger.TraceApi("SQL Database", "SchoolInterceptorLogging\\ScalarExecuted", stopWatch.Elapsed, $"Command Exceuted Successfully: {command.CommandText}");
            }
            base.ScalarExecuted(command, interceptionContext);
        }
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            stopWatch.Restart();
            base.ScalarExecuting(command, interceptionContext);
        }
    }
}