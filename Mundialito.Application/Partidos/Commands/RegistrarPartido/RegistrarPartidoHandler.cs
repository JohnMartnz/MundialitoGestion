using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Partidos.Commands.RegistrarPartido
{
    public class RegistrarPartidoHandler : IRequestHandler<RegistrarPartidoCommand, Result<Guid>>
    {
        private readonly IPartidoRepository _partidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarPartidoHandler(IPartidoRepository partidoRepository, IUnitOfWork unitOfWork)
        {
            _partidoRepository = partidoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RegistrarPartidoCommand request, CancellationToken cancellationToken)
        {
            var partido = await _partidoRepository.GetByIdAsync(request.PartidoId, cancellationToken);

            if(partido == null)
            {
                return Result<Guid>.Failure(Error.NotFound with { Description = "Partido no encontrado" });
            }

            if(partido.Estado == EstadoPartido.Finalizado)
            {
                return Result<Guid>.Failure(Error.Conflict with { Description = "El partido ya ha finalizado con un resultado" });
            }

            partido.RegistrarResultado(request.GolesLocal, request.GolesVisitante);

            await _partidoRepository.UpdateAsync(partido, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(partido.Id);
        }
    }
}
