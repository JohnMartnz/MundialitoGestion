using MediatR;
using Mundialito.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Queries.GetGoleadores
{
    public record GetGoleadoresQuery() : IRequest<Result<IEnumerable<GoleadorResponse>>>;
}
