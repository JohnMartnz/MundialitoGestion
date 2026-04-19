using Dapper;
using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mundialito.Application.Equipos.Queries.GetEquipos
{
    public class GetEquiposHandler : IRequestHandler<GetEquiposQuery,  Result<PagedResult<EquipoResponse>>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetEquiposHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<PagedResult<EquipoResponse>>> Handle(GetEquiposQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            int offset = (request.PageNumber - 1) * request.PageSize;


            const string sql = @"
                SELECT Id, Nombre FROM Equipos
                ORDER BY 
                    CASE WHEN @SortBy = 'Nombre' AND @SortDirection = 'ASC' THEN Nombre END ASC,
                    CASE WHEN @SortBy = 'Nombre' AND @SortDirection = 'DESC' THEN Nombre END DESC
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

                SELECT COUNT(*) FROM Equipos;
            ";

            using var multi = await connection.QueryMultipleAsync(sql, new {
                Offset = offset,
                PageSize = request.PageSize,
                SortBy = request.SortBy ?? "Nombre",
                SortDirection = request.SortDirection ?? "ASC"
            });

            var equipos = await multi.ReadAsync<EquipoResponse>();
            var totalRecords = await multi.ReadFirstAsync<int>();

            return Result<PagedResult<EquipoResponse>>.Success(new PagedResult<EquipoResponse>
            {
                Data = equipos,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = totalRecords
            });
        }
    }
}
