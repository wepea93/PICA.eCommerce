using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Products.Core.HealthChecks
{
    public class MemoryHealthCheck : IHealthCheck
    {
        private long Threshold { get; set; } = 1024L * 1024L;
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var allocate = GC.GetTotalMemory(forceFullCollection: false);
            var data = new Dictionary<string, object>()
            {
                {"AllocatedBytes" , allocate},
                {"Gen0Collections", GC.CollectionCount(0)},
                {"Gen1Collections", GC.CollectionCount(1)},
                {"Gen2Collections", GC.CollectionCount(2)},
            };

            var status = (allocate < Threshold) ? HealthStatus.Healthy : HealthStatus.Degraded;

            return Task.FromResult(new HealthCheckResult(status,
                description: "",
                exception: null,
                data: data));
        }
    }
}
