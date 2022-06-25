using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost
{
    public interface IGhostCommands : IService
    {
        void Show(ConstructionCard constructionCard);
        void Hide();
    }
}