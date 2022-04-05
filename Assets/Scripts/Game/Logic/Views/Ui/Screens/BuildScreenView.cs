using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView<BuildScreenPresenter>
    {
        public IButton CancelButton { get; }
        public IText Points { get; }
        public IText CurrentPoints { get; }
        public IProgressBar PointsProgress { get; set; }

        public BuildScreenView(ILevel level, IButton cancelButton, IText points, IText currentPoints, IProgressBar progressBar) : base(level)
        {
            CancelButton = cancelButton;
            Points = points;
            PointsProgress = progressBar;
            CurrentPoints = currentPoints;
        }
    }
}
