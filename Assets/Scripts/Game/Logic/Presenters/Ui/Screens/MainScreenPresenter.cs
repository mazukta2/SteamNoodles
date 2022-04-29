using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<IMainScreenView>
    {
        private IMainScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private Resources _resources;
        private ConstructionsManager _constructionsManager;

        public MainScreenPresenter(IMainScreenView view, ScreenManagerPresenter screenManager, 
            Resources resources, PlayerHand hand,
            ConstructionsManager constructionsManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));

            new HandPresenter(hand, screenManager, view.HandView);

            _resources.Points.OnPointsChanged += HandlePointsChanged;
            _view.NextWaveButton.SetAction(NextWaveClick);
            HandlePointsChanged();
        }

        protected override void DisposeInner()
        {
            _resources.Points.OnPointsChanged -= HandlePointsChanged;
        }

        private void HandlePointsChanged()
        {
            _view.Points.Value = $"{_resources.Points.Value}/{_resources.Points.PointsForNextLevel}";
            _view.PointsProgress.MainValue = _resources.Points.Progress;
        }

        private void NextWaveClick()
        {
            if (_constructionsManager.Placement.Constructions.Count < 2)
                throw new Exception("Not enough constructions");

            while (_constructionsManager.Placement.Constructions.Count > 1)
            {
                _constructionsManager.Placement.Constructions.Last().Destroy();
            }
        }

    }
}
