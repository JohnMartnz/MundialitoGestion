using Dapper;
using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Queries.GetEquipos
{
    public class GetEquiposHandler : IRequestHandler<GetEquiposQuery,  Result<IReadOnlyList<EquipoResponse>>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetEquiposHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<EquipoResponse>>> Handle(GetEquiposQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = "SELECT Id, Nombre FROM Equipos";

            var equipos = await connection.QueryAsync<EquipoResponse>(sql);

            return Result<IReadOnlyList<EquipoResponse>>.Success(equipos.AsList());
        }
    }
}
