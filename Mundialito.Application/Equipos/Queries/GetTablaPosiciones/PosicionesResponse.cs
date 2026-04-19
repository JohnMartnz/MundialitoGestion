using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Queries.GetTablaPosiciones
{
    public record PosicionesResponse
    {
        public string EquipoNombre { get; init; } = string.Empty;
        public int PartidosJugados { get; init; }
        public int PartidosGanados { get; init; }
        public int PartidosEmpatados { get; init; }
        public int PartidosPerdidos { get; init; }
        public int GolesAFavor { get; init; }
        public int GolesEnContra { get; init; }
        public int DiferenciaGoles => GolesAFavor - GolesEnContra;
        public int Puntos { get; init; }
    }
}
