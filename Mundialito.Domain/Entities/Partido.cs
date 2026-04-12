using Mundialito.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

public enum EstadoPartido
{
    Pendiente,
    EnCurso,
    Finalizado
}

namespace Mundialito.Domain.Entities
{
    public class Partido : BaseEntity
    {
        public Guid EquipoLocalId { get; private set; }
        public Guid EquipoVisitanteId { get; private set; }
        public Equipo? EquipoLocal { get; private set; }
        public Equipo? EquipoVisitante { get; private set; }
        public int GolesLocal { get; private set; }
        public int GolesVisitante { get; private set; }
        public DateTime Fecha { get; private set; }
        public EstadoPartido Estado { get; private set; }

        private Partido() { }

        public static Partido CrearPartido(Guid equipoLocalId, Guid equipoVisitanteId, DateTime Fecha)
        {
            return new Partido
            {
                Id = Guid.NewGuid(),
                EquipoLocalId = equipoLocalId,
                EquipoVisitanteId = equipoVisitanteId,
                Fecha = Fecha,
                Estado = EstadoPartido.Pendiente
            };
        }

        public void RegistrarResultado(int golesLocal, int golesVisitante)
        {
            GolesLocal = golesLocal;
            GolesVisitante = golesVisitante;
            Estado = EstadoPartido.Finalizado;
        }
    }
}
