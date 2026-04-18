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
            Console.WriteLine($"---> Middleware: {context.Request.Method} {context.Request.Path}");

            if(!context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("---> No es POST, pasando...");
                await _requestDelegate(context);
                return;
            }

            if(!context.Request.Headers.TryGetValue(IdempotencyHeader, out var idempotencyKeyValue)) {
                Console.WriteLine("---> No tiene Idempotency-Key, pasando...");
                await _requestDelegate(context);
                return;
            }

            Console.WriteLine($"---> Idempotency-Key encontrada: {idempotencyKeyValue}");

            if (!Guid.TryParse(idempotencyKeyValue, out var idempotencyKey))
            {
                Console.WriteLine("---> GUID inválido");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "El Idempotency-Key debe ser un GUID válido" });
                return;
            }

            Console.WriteLine($"---> Buscando key en DB: {idempotencyKey}");

            var existingRequest = await idempotencyRepository.GetByKeyAsync(idempotencyKey, context.RequestAborted);
            if(existingRequest is not null)
            {
                Console.WriteLine("---> Key encontrada, devolviendo respuesta guardada");
                context.Response.StatusCode = existingRequest.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(existingRequest.Response);
                return;
            }
            Console.WriteLine("---> Key nueva, procesando petición...");

            var originalBodyStream = context.Response.Body;
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _requestDelegate(context);
            Console.WriteLine($"---> Respuesta generada con StatusCode: {context.Response.StatusCode}");

            memoryStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

            Console.WriteLine($"---> ResponseBody: {responseBody}");

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
