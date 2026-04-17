using MediatR;
using Mundialito.Application.Common;
using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Partidos.Commands.RegistrarPartido
{
    public record RegistrarPartidoCommand(Guid PartidoId, int GolesLocal, int GolesVisitante) : IRequest<Result<Guid>>;
}
