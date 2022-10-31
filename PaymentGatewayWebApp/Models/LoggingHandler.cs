namespace PaymentGatewayWebApp.Models
{
    public class LoggingHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingHandler> _logger;

        public LoggingHandler(ILogger<LoggingHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Right before making http request");

            var response = base.SendAsync(request, cancellationToken).Result;

            _logger.LogInformation("Response has been received");

            return response;
        }
    }
}
