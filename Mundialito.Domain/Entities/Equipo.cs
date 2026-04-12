using Mundialito.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Domain.Entities
{
    public class Equipo : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;
        public ICollection<Jugador> Jugadores { get; private set; } = new List<Jugador>();

        private Equipo() { }

        public Equipo(string nombre)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
        }
    }
}
