namespace NodesTask.Middleware
{
    using System.Net;
    using System;
    using Newtonsoft.Json;
    using NodesTask.Models.Exceptions;
    using NodesTask.Interfaces;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ExceptionHandlingMiddleware(RequestDelegate next,
            IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var serviceManager = scope.ServiceProvider.GetRequiredService<IServiceManager>();
                    await HandleExceptionAsync(context, ex, serviceManager);
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, IServiceManager serviceManager)
        {
            var queryParams = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : string.Empty;
            var bodyParams = await GetRequestBodyAsync(context);
            var stackTrace = exception.StackTrace ?? string.Empty;
            var exceptionType = exception.GetType().Name;
            var responseContent = string.Empty;

            if (exception is SecureException secureException)
            {
                var journal = await serviceManager.ExceptionJournalService.LogExceptionAsync(exceptionType, queryParams, bodyParams, stackTrace);

                responseContent = JsonConvert.SerializeObject(new
                {
                    type = "Secure",
                    id = journal.EventId,
                    data = new { message = secureException.Message }
                });
            }
            else
            {
                var journal = await serviceManager.ExceptionJournalService.LogExceptionAsync("Exception", queryParams, bodyParams, stackTrace);

                responseContent = JsonConvert.SerializeObject(new
                {
                    type = "Exception",
                    id = journal.EventId,
                    data = new { message = $"Internal server error ID = {journal.EventId}" }
                });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(responseContent);
        }

        private async Task<string> GetRequestBodyAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var body = string.Empty;

            if (context.Request.ContentLength != null && context.Request.ContentLength > 0)
            {
                context.Request.Body.Position = 0;
                using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                {
                    body = await reader.ReadToEndAsync();
                }

                context.Request.Body.Position = 0;
            }

            return body;
        }
    }
}
