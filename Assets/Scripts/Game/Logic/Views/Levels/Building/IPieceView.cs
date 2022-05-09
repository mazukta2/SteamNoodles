using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Building
{
    public interface IPieceView : IView
    {
        IPosition Position { get; }
    }
}