using MediatR;
using Mundialito.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Queries.GetJugadores
{
    public record GetJugadoresQuery() : IRequest<Result<IReadOnlyList<JugadorResponse>>>;
}
