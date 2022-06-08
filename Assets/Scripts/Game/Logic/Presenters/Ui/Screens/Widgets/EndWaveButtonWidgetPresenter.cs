using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class EndWaveButtonWidgetPresenter : BasePresenter<IEndWaveButtonView>
    {
        private readonly IEndWaveButtonView _view;
        private IPresenterRepository<Construction> _constructions;
        private string _lastAnimation;
        private readonly StageWaveService _stageWaveService;


        public EndWaveButtonWidgetPresenter(IEndWaveButtonView view) : this(view, 
            IStageLevelPresenterRepositories.Default.Constructions, 
            IPresenterServices.Default.Get<StageWaveService>())
        {

        }

        public EndWaveButtonWidgetPresenter(IEndWaveButtonView view, 
            IPresenterRepository<Construction> constructions,
            StageWaveService stageWaveService)
            : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _stageWaveService = stageWaveService ?? throw new ArgumentNullException(nameof(stageWaveService));

            _view.NextWaveButton.SetAction(NextWaveClick);
            _view.FailWaveButton.SetAction(FailWaveClick);

            _constructions.OnAdded += HandleOnAdded;
            _stageWaveService.OnDayFinished += HandleOnDayFinished;

            UpdateWaveProgress();
        }

        protected override void DisposeInner()
        {
            _constructions.OnAdded -= HandleOnAdded;
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

        private void HandleOnAdded(EntityLink<Construction> cnst, Construction model)
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
