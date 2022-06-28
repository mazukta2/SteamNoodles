using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
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

        void Init(Uid constructionId)
        {
            new ConstructionPresenter(this, constructionId);
        }
    }
}
