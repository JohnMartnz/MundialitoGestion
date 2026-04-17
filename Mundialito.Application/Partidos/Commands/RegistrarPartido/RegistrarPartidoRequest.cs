using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Partidos.Commands.RegistrarPartido
{
    public record RegistrarPartidoRequest(int GolesLocal, int GolesVisitante);
}
