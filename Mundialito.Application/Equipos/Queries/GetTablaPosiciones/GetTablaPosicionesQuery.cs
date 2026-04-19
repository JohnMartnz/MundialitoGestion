using MediatR;
using Mundialito.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Queries.GetTablaPosiciones
{
    public record GetTablaPosicionesQuery() : IRequest<Result<IEnumerable<PosicionesResponse>>>;
}
