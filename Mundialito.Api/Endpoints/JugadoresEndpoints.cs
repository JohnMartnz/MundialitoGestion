using MediatR;
using Mundialito.Application.Jugadores.Commands.CrearJugador;

namespace Mundialito.Api.Endpoints
{
    public static class JugadoresEndpoints
    {
        public static void MapJugadoresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/jugadores").WithTags("Jugadores");

            group.MapPost("/", async (CrearJugadorRequest request, ISender sender) =>
            {
                var command = new CrearJugadorCommand(request.Nombre, request.Posicion, request.EquipoId);
                var result = await sender.Send(command);

                return result.IsSuccess
                    ? Results.Created($"/api/jugadores/{result.Value}", new { Id = result.Value })
                    : result.Error.Code == "409"
                        ? Results.Conflict(result.Error)
                        : Results.BadRequest(result.Error);
            })
            .WithName("CrearJugador")
            .WithSummary("Crea un nuevo jugador");
        }
    }
}
