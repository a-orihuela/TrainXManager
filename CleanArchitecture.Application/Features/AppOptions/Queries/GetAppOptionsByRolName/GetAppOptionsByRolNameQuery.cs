
using CleanArchitecture.Application.ViewModels;
using MediatR;

namespace CleanArchitecture.Application.Features.AppOptions.Queries.GetAppOptionsByRolName
{
    public class GetAppOptionsByRolNameQuery : IRequest<List<AppOptionsViewModel>>
    {
        public string? RolName { get; set; }

        public GetAppOptionsByRolNameQuery(string rolName)
        {
            RolName = rolName ?? throw new ArgumentNullException(nameof(rolName));
        }
    }
}
