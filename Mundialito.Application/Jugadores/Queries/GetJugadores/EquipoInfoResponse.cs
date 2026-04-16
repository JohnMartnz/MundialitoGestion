using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Queries.GetJugadores
{
    public record EquipoInfoResponse
    {
        public Guid Id { get; init; }
        public string Nombre {  get; init; } = string.Empty;
    }
}
