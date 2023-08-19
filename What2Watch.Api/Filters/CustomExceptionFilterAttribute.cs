namespace What2Watch.Api.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAppLogging<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(IWebHostEnvironment hostEnvironment, IAppLogging<CustomExceptionFilterAttribute> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            string stackTrace = _hostEnvironment.IsDevelopment() ? context.Exception.StackTrace : string.Empty;
            string message = ex.Message;
            string error;
            IActionResult actionResult;
            switch (ex)
            {
                case DbUpdateConcurrencyException ce:
                    error = "Concurrency Issue";
                    actionResult = new BadRequestObjectResult(new { Error = error, Message = message, StackTrace = stackTrace });
                    break;
                default:
                    error = "General error";
                    actionResult = new ObjectResult(new { Error = error, Message = message, StackTrace = stackTrace });
                    break;
            }

            _logger.LogAppError(ex, message);
            context.ExceptionHandled = true;
            context.Result = actionResult;
        }
    }
}
