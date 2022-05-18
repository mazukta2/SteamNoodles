using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<IMainScreenView>
    {
        private IMainScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private PlacementField _constructionsManager;
        private FlowManager _turnManager;

        private static string _lastAnimation;

        public MainScreenPresenter(IMainScreenView view, ScreenManagerPresenter screenManager,
            PlacementField constructionsManager,
            FlowManager turnManager, HandPresenter handPresenter) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _turnManager = turnManager ?? throw new ArgumentNullException(nameof(turnManager));

            handPresenter.Mode = HandPresenter.Modes.Choose;

            _constructionsManager.OnConstructionAdded += Placement_OnConstructionAdded;
            _view.NextWaveButton.SetAction(NextWaveClick);
            _view.FailWaveButton.SetAction(FailWaveClick);
            _turnManager.OnDayFinished += HandleOnDayFinished;
            UpdateWaveProgress();
        }

        protected override void DisposeInner()
        {
            _constructionsManager.OnConstructionAdded -= Placement_OnConstructionAdded;
            _turnManager.OnDayFinished -= HandleOnDayFinished;
        }

        private void NextWaveClick()
        {
            _turnManager.WinWave();
            UpdateWaveProgress();
        }

        private void FailWaveClick()
        {
            _turnManager.FailWave();
            UpdateWaveProgress();
        }

        private void HandleOnDayFinished()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IDayEndedScreenView>();
        }

        private void Placement_OnConstructionAdded(Construction obj)
        {
            UpdateWaveProgress();
        }

        private void UpdateWaveProgress()
        {
            _view.NextWaveButton.IsActive = _turnManager.CanNextWave();
            _view.FailWaveButton.IsActive = _turnManager.CanFailWave();
            _view.NextWaveProgress.Value = _turnManager.GetWaveProgress();

            var animation = GetCurrentWaveButtonAnimation().ToString();
            if (string.IsNullOrEmpty(_lastAnimation) || animation != _lastAnimation)
            {
                _lastAnimation = GetCurrentWaveButtonAnimation().ToString();
                _view.WaveButtonAnimator.Play(_lastAnimation);
            }
            else
            {
                _view.WaveButtonAnimator.SwitchTo(_lastAnimation);
            }
        }

        private WaveButtonAnimations GetCurrentWaveButtonAnimation()
        {
            if (_turnManager.CanFailWave())
                return WaveButtonAnimations.FailWave;
            else if (_turnManager.CanProcessNextWave())
                return WaveButtonAnimations.NextWave;
            else
                return WaveButtonAnimations.None;
        }

        public enum WaveButtonAnimations
        {
            None,
            NextWave,
            FailWave
        }
    }
}
