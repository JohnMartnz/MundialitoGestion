using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Partidos.Commands.CrearPartido
{
    public record CrearPartidoRequest(
        Guid EquipoLocalId,
        Guid EquipoVisitanteId, 
        DateTime Fecha
    );
}
