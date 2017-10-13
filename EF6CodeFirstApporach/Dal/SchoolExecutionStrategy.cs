using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EF6CodeFirstApporach.Dal
{
    /// <summary>
    /// This class will configure EF for retry options in case of transient errors(temporary errors). ex Database connectivity over Azure etc 
    /// https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/connection-resiliency-and-command-interception-with-the-entity-framework-in-an-asp-net-mvc-application
    /// </summary>
    internal class SchoolExecutionStrategy : DbExecutionStrategy
    {
        /// <summary>
        /// The default retry limit is 5, which means that the total amount of time spent 
        /// between retries is 26 seconds plus the random factor.
        /// </summary>
        public SchoolExecutionStrategy()
        {

        }

        public SchoolExecutionStrategy(int maxRetryCount, TimeSpan maxDelay) :base(maxRetryCount, maxDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            bool retry = false;
            var sqlException = exception as SqlException;
            if (sqlException!=null)
            {
                var errorNumberList = new List<int>() { 1250, -2, 2601 };
                if (sqlException.Errors.Cast<SqlError>().Any(x => errorNumberList.Contains(x.Number)))
                {
                    retry = true;
                }
                else
                {
                    Trace.WriteLine(exception.Message);
                }

            }

            if (exception is TimeoutException)
            {
                retry = true;
            }

            return retry;
        }
    }
}