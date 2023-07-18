using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain
{
    public class AppOption : BaseDomainModel
    {
        public string RolName { get; set; }
        public bool ShowFoodProgam { get; set; }
        public bool ShowTrainingProgam { get; set; }
        public bool ShowSocialNetwork { get; set; }
        public bool ShowChallenges { get; set; }
        public bool ShowAdvancedGraphics { get; set; }
    }
}
