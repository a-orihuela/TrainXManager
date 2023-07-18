using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.AppOptions.Command.Create
{
    public class CreateAppOptionsHandler : IRequestHandler<CreateAppOptionsCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailservice;
        private readonly ILogger<CreateAppOptionsHandler> _logger;

        public CreateAppOptionsHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailservice, ILogger<CreateAppOptionsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailservice = emailservice;
            _logger = logger;
        }

        public async Task<int> Handle(CreateAppOptionsCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AppOption>(request);
            _unitOfWork.Repository<AppOption>().AddEntity(entity);
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                throw new Exception($"No se pudo insertar el record de AppOption");
            }

            _logger.LogInformation($"AppOption {entity.Id} fue creado");
            await SendEmail(entity);
            return entity.Id;
        }

        private async Task SendEmail(AppOption item)
        {
            var email = new Email
            {
                To = "admin@gmail.com",
                Body = "Nuevo AppOption añadido al sistema",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailservice.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Errores enviando el email de {item.Id}");
            }
        }
    }
}
