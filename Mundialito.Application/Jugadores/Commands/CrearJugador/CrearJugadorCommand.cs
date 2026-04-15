using MediatR;
using Mundialito.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Commands.CrearJugador
{
    public record CrearJugadorCommand(string Nombre, string Posicion, Guid EquipoId) : IRequest<Result<Guid>>;
}
