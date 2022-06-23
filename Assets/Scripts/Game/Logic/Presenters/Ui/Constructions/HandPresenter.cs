using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        private readonly IPresenterRepository<ConstructionCard> _repository;
        private readonly GhostService _buildingService;
        private readonly ScreenService _screenService;
        private readonly IHandView _view;

        public HandPresenter(IHandView view)
            : this(view,
                  IPresenterServices.Default?.Get<IPresenterRepository<ConstructionCard>>(),
                  IPresenterServices.Default?.Get<GhostService>(),
                  IPresenterServices.Default?.Get<ScreenService>())
        {
        }

        public HandPresenter(IHandView view,
            IPresenterRepository<ConstructionCard> repository,
            GhostService buildingService,
            ScreenService screenService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));

            var cards = _repository.Get();
            foreach (var item in cards)
                HandleCardAdded(item);
            _repository.OnAdded += HandleCardAdded;

            _view.CancelButton.SetAction(CancelClick);
            _view.Animator.SwitchTo(Modes.Disabled.ToString());
            SetMode(Modes.Choose);
            _buildingService.OnShowed += HandleGhostShowed;
            _buildingService.OnHided += HandleGhostHided;
        }

        protected override void DisposeInner()
        {
            _buildingService.OnShowed -= HandleGhostShowed;
            _buildingService.OnHided -= HandleGhostHided;
            _repository.OnAdded -= HandleCardAdded;
        }

        private void HandleCardAdded(ConstructionCard obj)
        {
            var view = _view.Cards.Spawn<IHandConstructionView>(_view.CardPrototype);
            view.Init(obj);
        }

        private void CancelClick()
        {
            _screenService.Open<IMainScreenView>(x => x.Init());
        }

        private void HandleGhostHided()
        {
            SetMode(Modes.Choose);
        }

        private void HandleGhostShowed()
        {
            SetMode(Modes.Build);
        }

        private void SetMode(Modes value)
        {
            _view.Animator.Play(value.ToString());
        }

        public enum Modes
        {
            Disabled,
            Choose,
            Build
        }
    }
}
