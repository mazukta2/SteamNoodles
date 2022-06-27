using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels
{
    public interface IConstructionView : IViewWithPresenter
    {
        IPosition Position { get;}
        IRotator Rotator { get; }
        IViewContainer Container { get; }
        IViewContainer EffectsContainer { get; }
        IViewPrefab ExplosionPrototype { get; }

        void Init(IDataProvider<ConstructionPresenterData> construction)
        {
            new ConstructionPresenter(this, construction);
        }
    }
}
