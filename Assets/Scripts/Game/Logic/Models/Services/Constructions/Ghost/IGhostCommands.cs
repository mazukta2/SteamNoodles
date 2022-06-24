using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public interface IGhostCommands : IService
    {
        void Show(ConstructionCard constructionCard);
        void Hide();
    }
}