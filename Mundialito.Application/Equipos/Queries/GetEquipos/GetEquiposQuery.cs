using MediatR;
using Mundialito.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Queries.GetEquipos
{
    public record GetEquiposQuery() : IRequest<Result<IReadOnlyList<EquipoResponse>>>;
}
