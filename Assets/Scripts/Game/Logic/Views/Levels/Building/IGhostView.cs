using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IGhostView : IPresenterView
    {
        ILevelPosition LocalPosition { get; }
        IViewContainer Container { get; }
        IRotator Rotator { get; }
    }
}
