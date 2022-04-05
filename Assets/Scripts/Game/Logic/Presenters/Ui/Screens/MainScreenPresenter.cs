using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<MainScreenView>
    {
        private MainScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private Resources _resources;

        public MainScreenPresenter(MainScreenView view, ScreenManagerPresenter screenManager, Resources resources) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));

            new HandPresenter(view.HandView.Level.Model.Hand, screenManager, view.HandView);

            _resources.Points.OnPointsChanged += HandlePointsChanged;
            HandlePointsChanged();
        }

        protected override void DisposeInner()
        {
            _resources.Points.OnPointsChanged -= HandlePointsChanged;
        }

        private void HandlePointsChanged()
        {
            _view.Points.Value = $"{_resources.Points.Value}/{_resources.Points.PointsForNextLevel}";
            _view.PointsProgress.Value = _resources.Points.Progress;
        }

    }
}
