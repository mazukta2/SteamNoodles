using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
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
        private PlacementField _constructionsManager;
        private FlowManager _turnManager;

        public MainScreenPresenter(IMainScreenView view, ScreenManagerPresenter screenManager, 
            Resources resources, PlayerHand hand,
            PlacementField constructionsManager,
            LevelDefinition levelDefinition,
            FlowManager turnManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _turnManager = turnManager ?? throw new ArgumentNullException(nameof(turnManager));

            new HandPresenter(hand, screenManager, view.HandView);

            _resources.Points.OnPointsChanged += HandlePointsChanged;
            _constructionsManager.OnConstructionAdded += Placement_OnConstructionAdded;
            _view.NextWaveButton.SetAction(NextWaveClick);
            _view.NextWaveButton.IsActive = _turnManager.CanNextWaveClick();
            UpdateNextWaveProgress();
            HandlePointsChanged();
        }

        protected override void DisposeInner()
        {
            _resources.Points.OnPointsChanged -= HandlePointsChanged;
            _constructionsManager.OnConstructionAdded -= Placement_OnConstructionAdded;
        }

        private void HandlePointsChanged()
        {
            _view.Points.Value = $"{_resources.Points.Value}/{_resources.Points.PointsForNextLevel}";
            _view.PointsProgress.MainValue = _resources.Points.Progress;
        }

        private void NextWaveClick()
        {
            _turnManager.NextWave();
        }

        private void Placement_OnConstructionAdded(Construction obj)
        {
            _view.NextWaveButton.IsActive = _turnManager.CanNextWaveClick();
            UpdateNextWaveProgress();
        }

        private void UpdateNextWaveProgress()
        {
            _view.NextWaveProgress.Value = _turnManager.GetWaveProgress();
        }

    }
}
