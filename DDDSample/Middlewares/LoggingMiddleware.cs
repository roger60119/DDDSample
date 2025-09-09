using System.Text;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            // ���� Request
            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true).ReadToEndAsync();
            context.Request.Body.Position = 0;
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} {requestBody}");
        }
        else
        {
            // �p�G���O API ���|�A�����I�s�U�@�� Middleware
            await _next(context);
            return;
        }

        // ���� Response
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        _logger.LogInformation($"Response: {context.Response.StatusCode} {responseText}");

        await responseBody.CopyToAsync(originalBodyStream);
    }
}