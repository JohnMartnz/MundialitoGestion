using MediatR;
using Mundialito.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Partidos.Commands.CrearPartido
{
    public record CrearPartidoCommand(
            Guid EquipoLocalId,
            Guid EquipoVisitanteId,
            DateTime Fecha
        ) : IRequest<Result<Guid>>;
}
