using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Building
{
    public interface IGhostView : IViewWithPresenter
    {
        IPosition LocalPosition { get; }
        IViewContainer Container { get; }
        IRotator Rotator { get; }
    }
}
