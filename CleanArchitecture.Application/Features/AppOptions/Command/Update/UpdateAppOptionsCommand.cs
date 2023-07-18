using MediatR;

namespace CleanArchitecture.Application.Features.AppOptions.Command.Update
{
    public class UpdateAppOptionsCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool ShowFoodProgam { get; set; } = false;
        public bool ShowTrainingProgam { get; set; } = false;
        public bool ShowSocialNetwork { get; set; } = false;
        public bool ShowChallenges { get; set; } = false;
        public bool ShowAdvancedGraphics { get; set; } = false;
    }
}
