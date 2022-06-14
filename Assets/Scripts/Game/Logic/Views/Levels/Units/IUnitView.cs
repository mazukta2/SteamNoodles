using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Units
{
    public interface IUnitView : IViewWithPresenter
    {
        IPosition Position { get; }
        IRotator Rotator { get; }
        IAnimator Animator { get; }
        IUnitDresser UnitDresser { get; }
        IViewContainer SmokeContainer { get; }
        IViewPrefab SmokePrefab { get; }
    }
}