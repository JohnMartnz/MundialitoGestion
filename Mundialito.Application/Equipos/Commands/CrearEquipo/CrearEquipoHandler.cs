using MediatR;
using Mundialito.Application.Common;
using Mundialito.Application.Common.Interfaces;
using Mundialito.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mundialito.Application.Equipos.Commands.CrearEquipo
{
    public class CrearEquipoHandler : IRequestHandler<CrearEquipoCommand, Result<Guid>>
    {
        private readonly IEquipoRepository _equipoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CrearEquipoHandler(IEquipoRepository equipoRepository, IUnitOfWork unitOfWork)
        {
            _equipoRepository = equipoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CrearEquipoCommand request, CancellationToken cancellationToken)
        {
            var nameExist = await _equipoRepository.ExisteNombreAsync(request.Nombre, cancellationToken);

            if(nameExist)
            {
                return Result<Guid>.Failure(Error.Conflict);
            }

            var equipo = new Equipo(request.Nombre);

            await _equipoRepository.AddAsync(equipo, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<Guid>.Success(equipo.Id);
        }
    }
}
