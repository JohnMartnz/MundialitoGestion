using MediatR;
using Mundialito.Application.Equipos.Commands.CrearEquipo;
using Mundialito.Application.Equipos.Queries.GetEquipos;

namespace Mundialito.Api.Endpoints
{
    public static class EquiposEndpoints
    {
        public static void MapEquiposEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/equipos").WithTags("Equipos");

            group.MapPost("/", async (CrearEquipoRequest request, ISender sender) =>
            {
                var command = new CrearEquipoCommand(request.Nombre);
                var result = await sender.Send(command);

                return result.IsSuccess
                ? Results.Json(result.Value, statusCode: 201)
                //? Results.Created($"/api/equipos/{result.Value}", new { id = result.Value })
                : result.Error.Code == "409"
                    ? Results.Conflict(result.Error)
                    : Results.BadRequest(result.Error);
            }).WithName("CrearEquipo")
            .WithSummary("Crea un nuevo equipo");

            group.MapGet("/", async (
                ISender sender,
                int pageNumber = 1,
                int pageSize = 2,
                string? sortBy = null,
                string? sortDirection = "asc"
            ) =>
            {
                var query = new GetEquiposQuery(pageNumber, pageSize, sortBy, sortDirection);
                var result = await sender.Send(query);

                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.Error);
            }).WithName("GetEquipos")
            .WithSummary("Obtiene la lista de equipos");
        }
    }
}
