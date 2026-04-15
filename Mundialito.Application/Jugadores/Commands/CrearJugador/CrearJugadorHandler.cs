using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Jugadores.Commands.CrearJugador
{
    public class CrearJugadorHandler : IRequestHandler<CrearJugadorCommand, Result<Guid>>
    {
        private readonly IJugadorRepository _jugadorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CrearJugadorHandler(IJugadorRepository jugadorRepository, IUnitOfWork unitOfWork)
        {
            _jugadorRepository = jugadorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CrearJugadorCommand request, CancellationToken cancellationToken)
        {
            var equipoExists = await _jugadorRepository.EquipoExistsAsync(request.EquipoId, cancellationToken);

            if (!equipoExists)
            {
                return Result<Guid>.Failure(Error.NotFound with { Description = "No se encontró el equipo"});
            }

            var isEquipoAvailableToAddJugadores = await _jugadorRepository.IsEquipoAvailableToAddJugadores(request.EquipoId, cancellationToken);

            if(!isEquipoAvailableToAddJugadores)
            {
                return Result<Guid>.Failure(Error.Conflict with { Description = "El equipo ya tiene el número máximo de jugadores permitido." });
            }

            var jugador = new Jugador(request.Nombre, request.Posicion, request.EquipoId);

            await _jugadorRepository.AddAsync(jugador, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(jugador.Id);
        }
    }
}
