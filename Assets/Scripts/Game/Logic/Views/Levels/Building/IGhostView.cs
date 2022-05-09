using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IGhostView : IPresenterView
    {
        ILevelPosition LocalPosition { get; }
        IViewContainer Container { get; }
        IRotator Rotator { get; }
    }
}
