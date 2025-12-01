using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ParkApp.BusinessLogic;

public class ParkAppExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ParkAppExceptionHandler> _logger;

    public ParkAppExceptionHandler(ILogger<ParkAppExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error ocurred on {method} with body {body}", httpContext.Request.Method, httpContext.Request.Body);

        var details = new ProblemDetails
        {
            Title = "An error ocurred",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "Internal server error"
        };

        httpContext.Response.StatusCode = details.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(details, cancellationToken);

        return true;
    }
}
