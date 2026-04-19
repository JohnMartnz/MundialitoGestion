using Dapper;
using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Queries.GetGoleadores
{
    public class GetGoleadoresHandler : IRequestHandler<GetGoleadoresQuery, Result<IEnumerable<GoleadorResponse>>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetGoleadoresHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IEnumerable<GoleadorResponse>>> Handle(GetGoleadoresQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string query = @"
                SELECT j.Nombre, e.Nombre AS EquipoNombre, j.Goles
                FROM Jugadores j
                INNER JOIN Equipos e ON j.EquipoId = e.Id
                WHERE j.Goles > 0
                ORDER BY j.Goles DESC
            ";

            var result = await connection.QueryAsync<GoleadorResponse>(query);

            return Result<IEnumerable<GoleadorResponse>>.Success(result);
        }
    }
}
