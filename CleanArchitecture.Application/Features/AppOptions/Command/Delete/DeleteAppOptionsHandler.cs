using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.AppOptions.Command.Delete
{
    public class DeleteAppOptionsHandler : IRequestHandler<DeleteAppOptionsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAppOptionsHandler> _logger;

        public DeleteAppOptionsHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteAppOptionsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteAppOptionsCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Repository<AppOption>().GetByIdAsync(request.Id);
            if (item == null)
            {
                _logger.LogError($"AppOption {request.Id} no existe en el sistema");
                throw new NotFoundException(nameof(AppOption), request.Id);
            }

            _unitOfWork.Repository<AppOption>().DeleteEntity(item);
            await _unitOfWork.Complete();
            _logger.LogInformation($"El AppOption {request.Id} fue eliminado con exito");

            return Unit.Value;
        }
    }
}
