using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost
{
    public interface IGhostCommands : IService
    {
        GhostPresentation Show(Uid constructionCardId);
        void Hide();
    }
}