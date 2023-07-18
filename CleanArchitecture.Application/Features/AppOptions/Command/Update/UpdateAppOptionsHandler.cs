using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.AppOptions.Command.Update
{
    public class UpdateAppOptionsHandler : IRequestHandler<UpdateAppOptionsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAppOptionsHandler> _logger;

        public UpdateAppOptionsHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateAppOptionsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateAppOptionsCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWork.Repository<AppOption>().GetByIdAsync(request.Id);
            if (item == null)
            {
                _logger.LogError($"AppOption {request.Id} no existe en el sistema");
                throw new NotFoundException(nameof(AppOption), request.Id);
            }

            _mapper.Map(request, item, typeof(UpdateAppOptionsCommand), typeof(AppOption));
            _unitOfWork.Repository<AppOption>().UpdateEntity(item);
            await _unitOfWork.Complete();
            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer {request.Id}");

            return Unit.Value;
        }
    }
}
