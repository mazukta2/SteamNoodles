using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level
{
    public interface IGameLevelPresenterRepository
    {
        static IGameLevelPresenterRepository Default { get; set; }

        IPresenterRepository<ConstructionCard> Cards { get; }
        IPresenterRepository<Construction> Constructions { get; }
    }
}
