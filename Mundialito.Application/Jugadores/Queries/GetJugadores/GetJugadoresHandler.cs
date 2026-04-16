using Dapper;
using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Queries.GetJugadores
{
    public class GetJugadoresHandler : IRequestHandler<GetJugadoresQuery, Result<IReadOnlyList<JugadorResponse>>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetJugadoresHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<JugadorResponse>>> Handle(GetJugadoresQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string query = @"
                SELECT 
                    j.Id, 
                    j.Nombre, 
                    j.Posicion,
                    e.Id,
                    e.Nombre
                FROM Jugadores j
                INNER JOIN Equipos e ON j.EquipoId = e.Id";

            var jugadores = await connection.QueryAsync<JugadorResponse, EquipoInfoResponse, JugadorResponse>(
                query,
                (jugador, equipo) => jugador with { Equipo = equipo },
                splitOn: "Id"
            );

            return Result<IReadOnlyList<JugadorResponse>>.Success(jugadores.AsList());
        }
    }
}
