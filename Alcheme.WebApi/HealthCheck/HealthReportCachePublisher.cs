using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Alcheme.WebApi
{
    public class HealthReportCachePublisher : IHealthCheckPublisher
    {
        /// <summary>
        /// The latest health report which got published
        /// </summary>
        public static HealthReport Latest { get; set; }

        /// <summary>
        /// Publishes a provided report
        /// </summary>
        /// <param name="report">The result of executing a set of health checks</param>
        /// <param name="cancellationToken">A task which will complete when publishing is complete</param>
        /// <returns></returns>
        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            Latest = report;

            return Task.CompletedTask;
        }
    }
}
