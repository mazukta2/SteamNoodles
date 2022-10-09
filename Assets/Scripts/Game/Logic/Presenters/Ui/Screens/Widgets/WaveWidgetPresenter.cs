using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class WaveWidgetPresenter : BasePresenter<IWaveWidgetView>
    {
        private IWaveWidgetView _view;
        private PlacementField _constructionsManager;
        private FlowManager _turnManager;
        private string _lastAnimation;

        private static bool _isEnabled;
        private static List<WaveWidgetPresenter> _presenters = new List<WaveWidgetPresenter>();

        public WaveWidgetPresenter(IWaveWidgetView view) : this(view,
            IMainLevel.Default.Constructions, IMainLevel.Default.TurnManager)
        {
        }

        public WaveWidgetPresenter(IWaveWidgetView view, PlacementField constructionsManager, FlowManager turnManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _turnManager = turnManager ?? throw new ArgumentNullException(nameof(turnManager));

            _constructionsManager.OnConstructionAdded += Placement_OnConstructionAdded;
            _constructionsManager.OnConstructionRemoved += _constructionsManager_OnConstructionRemoved;
            _turnManager.OnWaveEnded += _turnManager_OnWaveEnded;
            _view.NextWaveButton.SetAction(NextWaveClick);
            _view.FailWaveButton.SetAction(FailWaveClick);
            UpdateWaveProgress();

            _presenters.Add(this);
        }


        protected override void DisposeInner()
        {
            _constructionsManager.OnConstructionAdded -= Placement_OnConstructionAdded;
            _constructionsManager.OnConstructionRemoved -= _constructionsManager_OnConstructionRemoved;
            _turnManager.OnWaveEnded -= _turnManager_OnWaveEnded;

            _presenters.Remove(this);
        }

        public static void SetEnabled(bool isEnabled)
        {
            _isEnabled = isEnabled;
            foreach (var item in _presenters)
                item.UpdateWaveProgress();
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

        private void Placement_OnConstructionAdded(Construction obj)
        {
            UpdateWaveProgress();
        }

        private void _constructionsManager_OnConstructionRemoved(Construction obj)
        {
            UpdateWaveProgress();
        }


        private void _turnManager_OnWaveEnded(bool obj)
        {
            UpdateWaveProgress();
        }

        private void UpdateWaveProgress()
        {
            if (IsDisposed)
                return;

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
            if (!_isEnabled)
                return WaveButtonAnimations.None;

            if (_turnManager.CanFailWave())
                return WaveButtonAnimations.FailWave;

            return WaveButtonAnimations.NextWave;
        }

        public enum WaveButtonAnimations
        {
            None,
            NextWave,
            FailWave
        }
    }
}
