using MediatR;

namespace CleanArchitecture.Application.Features.AppOptions.Command.Delete
{
    public class DeleteAppOptionsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
