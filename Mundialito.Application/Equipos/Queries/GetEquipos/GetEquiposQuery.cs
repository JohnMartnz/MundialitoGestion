using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Queries.GetEquipos
{
    public record GetEquiposQuery(
        int PageNumber = 1,
        int PageSize = 2,
        string? SortBy = null,
        string? SortDirection = null
    ) : IRequest<Result<PagedResult<EquipoResponse>>>;
}
