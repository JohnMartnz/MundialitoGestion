using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Queries.GetGoleadores
{
    public record GoleadorResponse
    {
        public string Nombre { get; init; } = string.Empty;
        public string EquipoNombre { get; init; } = string.Empty;
        public int Goles { get; init; }
    }
}
