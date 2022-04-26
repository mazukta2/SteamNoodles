using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IBuildScreenView : IScreenView
    {
        IButton CancelButton { get; }
        IWorldText Points { get; }
        IText CurrentPoints { get; }
        IProgressBar PointsProgress { get; }
        BuildScreenPresenter Presenter { get; }
    }
}
