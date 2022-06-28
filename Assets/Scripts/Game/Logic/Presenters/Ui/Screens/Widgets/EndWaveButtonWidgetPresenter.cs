using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Building;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Services.Flow;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class EndWaveButtonWidgetPresenter : BasePresenter<IEndWaveButtonView>
    {
        private readonly IEndWaveButtonView _view;
        private FieldConstructionsRepository _fieldConstructions;
        private string _lastAnimation;
        private readonly StageWaveService _stageWaveService;


        public EndWaveButtonWidgetPresenter(IEndWaveButtonView view) : this(view, 
            IPresenterServices.Default?.Get<FieldConstructionsRepository>(),
            IPresenterServices.Default.Get<StageWaveService>())
        {

        }

        public EndWaveButtonWidgetPresenter(IEndWaveButtonView view, 
            FieldConstructionsRepository fieldConstructions,
            StageWaveService stageWaveService)
            : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _fieldConstructions = fieldConstructions ?? throw new ArgumentNullException(nameof(fieldConstructions));
            _stageWaveService = stageWaveService ?? throw new ArgumentNullException(nameof(stageWaveService));

            _view.NextWaveButton.SetAction(NextWaveClick);
            _view.FailWaveButton.SetAction(FailWaveClick);

            //_constructions.OnAdded += HandleOnAdded;
            _stageWaveService.OnDayFinished += HandleOnDayFinished;

            UpdateWaveProgress();
        }

        protected override void DisposeInner()
        {
            //_constructions.OnAdded -= HandleOnAdded;
            _stageWaveService.OnDayFinished -= HandleOnDayFinished;
        }

        private void NextWaveClick()
        {
            _stageWaveService.WinWave();
            UpdateWaveProgress();
        }

        private void FailWaveClick()
        {
            _stageWaveService.FailWave();
            UpdateWaveProgress();
        }

        private void HandleOnDayFinished()
        {
            //ScreenManagerPresenter.Default.GetCollection<CommonScreens>().Open<IDayEndedScreenView>();
        }

        private void HandleOnAdded(FieldConstruction model)
        {
            UpdateWaveProgress();
        }

        private void UpdateWaveProgress()
        {
            _view.NextWaveButton.IsActive = _stageWaveService.CanWinWave();
            _view.FailWaveButton.IsActive = _stageWaveService.CanFailWave();
            _view.NextWaveProgress.Value = _stageWaveService.GetWaveProgress();

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
            if (_stageWaveService.CanFailWave())
                return WaveButtonAnimations.FailWave;
            else if (_stageWaveService.IsActive())
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
