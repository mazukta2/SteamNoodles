using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IConstructionView : IViewWithPresenter
    {
        IPosition Position { get;}
        IRotator Rotator { get; }
        IViewContainer Container { get; }
        IViewContainer EffectsContainer { get; }
        IViewPrefab ExplosionPrototype { get; }
    }
}
