using Assets.Scripts.Models.Buildings;

namespace Assets.Scripts.Models.Requests
{
    public interface IBuildingsRequest : IRequestMessage
    {
        SessionBuildings Buildings { get; set; }
    }
}
