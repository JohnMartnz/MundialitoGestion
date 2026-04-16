using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Queries.GetJugadores
{
    public record JugadorResponse
    {
        public Guid Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string Posicion { get; init; } = string.Empty;
        public EquipoInfoResponse? Equipo { get; init; }
    }
}
