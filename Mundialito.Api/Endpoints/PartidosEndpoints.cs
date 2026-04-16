using MediatR;
using Mundialito.Application.Partidos.Commands.CrearPartido;

namespace Mundialito.Api.Endpoints
{
    public static class PartidosEndpoints
    {
        public static void MapPartidosEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/partidos").WithTags("Partidos");

            group.MapPost("/", async (CrearPartidoRequest request, ISender sender) =>
            {
                var command = new CrearPartidoCommand(request.EquipoLocalId, request.EquipoVisitanteId, request.Fecha);
                var result = await sender.Send(command);

                return result.IsSuccess
                    ? Results.Created($"/api/partidos/{result.Value}", result.Value)
                    : result.Error.Code == "409"
                        ? Results.Conflict(result.Error)
                        : Results.BadRequest(result.Error);
            })
            .WithName("CrearPartido")
            .WithSummary("Crea un nuevo partido entre dos equipos en una fecha específica.");

        }
    }
}
