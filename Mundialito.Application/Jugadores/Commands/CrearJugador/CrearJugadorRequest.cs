using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Commands.CrearJugador
{
    public record CrearJugadorRequest(string Nombre, string Posicion, Guid EquipoId);
}
