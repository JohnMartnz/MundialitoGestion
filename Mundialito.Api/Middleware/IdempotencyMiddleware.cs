using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;

namespace Mundialito.Api.Middleware
{
    public class IdempotencyMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private const string IdempotencyHeader = "Idempotency-Key";

        public IdempotencyMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context, IIdempotencyRepository idempotencyRepository)
        {
            if(!context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                await _requestDelegate(context);
                return;
            }

            if(!context.Request.Headers.TryGetValue(IdempotencyHeader, out var idempotencyKeyValue)) {
                await _requestDelegate(context);
                return;
            }

            if (!Guid.TryParse(idempotencyKeyValue, out var idempotencyKey))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "El Idempotency-Key debe ser un GUID válido" });
                return;
            }

            var existingRequest = await idempotencyRepository.GetByKeyAsync(idempotencyKey, context.RequestAborted);
            if(existingRequest is not null)
            {
                context.Response.StatusCode = existingRequest.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(existingRequest.Response);
                return;
            }

            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _requestDelegate(context);

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            var idempotencyRequest = new IdempotencyRequest(
                key: idempotencyKey,
                response: responseBody,
                statusCode: context.Response.StatusCode
            );
            await idempotencyRepository.AddAsync(idempotencyRequest, context.RequestAborted);

            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
            context.Response.Body = originalBodyStream;
        }
    }
}
