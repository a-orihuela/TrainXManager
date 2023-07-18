using MediatR;

namespace CleanArchitecture.Application.Features.AppOptions.Command.Create
{
    public class CreateAppOptionsCommand : IRequest<int>
    {
        public string RolName { get; set; } = string.Empty;
        public bool ShowFoodProgam { get; set; } = false;
        public bool ShowTrainingProgam { get; set; } = false;
        public bool ShowSocialNetwork { get; set; } = false;
        public bool ShowChallenges { get; set; } = false;
        public bool ShowAdvancedGraphics { get; set; } = false;
    }
}
