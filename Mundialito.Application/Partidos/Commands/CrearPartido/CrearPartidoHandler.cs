using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Partidos.Commands.CrearPartido
{
    public class CrearPartidoHandler : IRequestHandler<CrearPartidoCommand, Result<Guid>>
    {
        private readonly IPartidoRepository _partidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CrearPartidoHandler(IPartidoRepository partidoRepository, IUnitOfWork unitOfWork)
        {
            _partidoRepository = partidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CrearPartidoCommand request, CancellationToken cancellationToken)
        {
            if(request.EquipoLocalId == request.EquipoVisitanteId)
            {
                return Result<Guid>.Failure(Error.Conflict with { Description = "Los equipos no pueden ser el mismo."});
            }

            var localExists = await _partidoRepository.EquipoExistsAsync(request.EquipoLocalId, cancellationToken);
            var visitanteExists = await _partidoRepository.EquipoExistsAsync(request.EquipoVisitanteId, cancellationToken);

            if(!localExists || !visitanteExists)
            {
                return Result<Guid>.Failure(Error.NotFound with { Description = "Uno o ambos equipos no existen."});
            }

            var partido = Partido.CrearPartido(
                request.EquipoLocalId,
                request.EquipoVisitanteId, 
                request.Fecha
            );

            await _partidoRepository.AddAsync(partido, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(partido.Id);
        }
    }
}
