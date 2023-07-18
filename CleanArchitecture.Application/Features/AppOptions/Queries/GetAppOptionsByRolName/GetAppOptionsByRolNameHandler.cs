using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain;
using MediatR;

namespace CleanArchitecture.Application.Features.AppOptions.Queries.GetAppOptionsByRolName
{
    public class GetAppOptionsByRolNameHandler : IRequestHandler<GetAppOptionsByRolNameQuery, List<AppOptionsViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAppOptionsByRolNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AppOptionsViewModel>> Handle(GetAppOptionsByRolNameQuery request, CancellationToken cancellationToken)
        {
            var streamerList = await _unitOfWork.Repository<AppOption>().GetAsync(
                b => b.RolName == request.RolName, b => b.OrderBy(x => x.CreatedDate));
            return _mapper.Map<List<AppOptionsViewModel>>(streamerList);
        }
    }
}
