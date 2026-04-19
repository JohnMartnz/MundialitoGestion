using Dapper;
using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Queries.GetTablaPosiciones
{
    public class GetTablaPosicionesHandler : IRequestHandler<GetTablaPosicionesQuery, Result<IEnumerable<PosicionesResponse>>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetTablaPosicionesHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IEnumerable<PosicionesResponse>>> Handle(GetTablaPosicionesQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = @"
                WITH Resultados AS (
                    SELECT
                        EquipoLocalId AS EquipoId,
                        GolesLocal AS GolesAFavor,
                        GolesVisitante AS GolesEnContra,
                        CASE WHEN GolesLocal > GolesVisitante THEN 3
                            WHEN GolesLocal = GolesVisitante THEN 1 ELSE 0 END AS Puntos
                    FROM Partidos WHERE Estado = 1
                    UNION ALL
                    SELECT
                        EquipoVisitanteId AS EquipoId,
                        GolesVisitante AS GolesAFavor,
                        GolesLocal AS GolesEnContra,
                        CASE WHEN GolesVisitante > GolesLocal THEN 3
                            WHEN GolesVisitante = GolesLocal THEN 1 ELSE 0 END AS Puntos
                    FROM Partidos WHERE Estado = @estado
                )
                SELECT
                    e.Nombre AS EquipoNombre,
                    COUNT(r.EquipoId) AS PartidosJugados,
                    SUM(CASE WHEN r.Puntos = 3 THEN 1 ELSE 0 END) AS PartidosGanados,
                    SUM(CASE WHEN r.Puntos = 1 THEN 1 ELSE 0 END) AS PartidosEmpatados,
                    SUM(CASE WHEN r.Puntos = 0 THEN 1 ELSE 0 END) AS PartidosPerdidos,
                    SUM(r.GolesAFavor) AS GolesAFavor,
                    SUM(r.GolesEnContra) AS GolesEnContra,
                    SUM(r.GolesAFavor) - SUM(r.GolesEnContra) AS DiferenciaGoles,
                    SUM(r.Puntos) AS Puntos
                FROM Equipos e
                LEFT JOIN Resultados r ON e.Id = r.EquipoId
                GROUP BY e.Id, e.Nombre
                ORDER BY Puntos DESC, (SUM(r.GolesAFavor) - SUM(r.GolesEnContra)) DESC, SUM(r.GolesAFavor) DESC
            ";

            var tabla = await connection.QueryAsync<PosicionesResponse>(sql, new { estado = (int)EstadoPartido.Finalizado});

            return Result<IEnumerable<PosicionesResponse>>.Success(tabla);
        }
    }
}
