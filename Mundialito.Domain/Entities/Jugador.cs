using Mundialito.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Domain.Entities
{
    public class Jugador : BaseEntity
    {
        public string Nombre { get; private set; } = string.Empty;
        public string Posicion { get; private set; } = string.Empty;
        public int Goles { get; private set; }
        public Guid EquipoId { get; private set; }
        public Equipo? Equipo { get; private set; }

        private Jugador() { }

        public Jugador(string nombre, string posicion, Guid equipoId)
        {
            Id = Guid.NewGuid();
            Nombre = nombre;
            Posicion = posicion;
            EquipoId = equipoId;
            Goles = 0;
        }

        public void AnotarGol()
        {
            Goles++;
        }
    }
}
