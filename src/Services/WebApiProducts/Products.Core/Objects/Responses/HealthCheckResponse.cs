
namespace Products.Core.Objects.Responses
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }
        public IEnumerable<IndividualHealthCheckResponse> HealtChecks { get; set; }
        public TimeSpan HealthCheckDuration { get; set; }
    }
}
