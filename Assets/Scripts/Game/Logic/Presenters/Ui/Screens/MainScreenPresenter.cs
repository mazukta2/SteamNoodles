using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BaseGameScreenPresenter
    {
        private MainScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private Resources _resources;

        public MainScreenPresenter(MainScreenView view, ScreenManagerPresenter screenManager, Resources resources) : base(screenManager, view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            view.HandView.SetScreen(this);

            _resources.Points.OnPointsChanged += HandlePointsChanged;
            HandlePointsChanged();
        }

        protected override void DisposeInner()
        {
            _resources.Points.OnPointsChanged -= HandlePointsChanged;
        }

        private void HandlePointsChanged()
        {
            _view.Points.Value = _resources.Points.Value.ToString();
            _view.PointsProgress.Value = _resources.Points.Progress;
        }

    }
}
