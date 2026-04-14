using MediatR;
using Mundialito.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Commands.CrearEquipo
{
    public record CrearEquipoCommand(string Nombre) : IRequest<Result<Guid>>;
}
