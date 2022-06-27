using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Repositories.Aggregations.Constructions
{
    public class GhostPresentationRepository : IGhostCommands
    {
        public GhostPresentation Show(Uid constructionCardId)
        {
            //_ghost.Add(new ConstructionGhost(constructionCard, new FieldPosition(_field)));
            return null;
        }

        public void Hide()
        {
            // if (Has())
            //     Remove();
        }
    }
}