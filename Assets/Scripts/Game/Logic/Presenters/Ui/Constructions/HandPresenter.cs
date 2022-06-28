using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        private readonly ConstructionCardsRepository _cards;
        private readonly GhostRepository _ghostRepository;
        private readonly ScreenService _screenService;
        private readonly IHandView _view;

        public HandPresenter(IHandView view)
            : this(view,
                  IPresenterServices.Default?.Get<ConstructionCardsRepository>(),
                  IPresenterServices.Default?.Get<GhostRepository>(),
                  IPresenterServices.Default?.Get<ScreenService>())
        {
        }

        public HandPresenter(IHandView view,
            ConstructionCardsRepository repository,
            GhostRepository ghostRepository,
            ScreenService screenService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _cards = repository ?? throw new ArgumentNullException(nameof(repository));
            _ghostRepository = ghostRepository ?? throw new ArgumentNullException(nameof(ghostRepository));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));

            // var cards = _cards.Get();
            // foreach (var item in cards)
            //     HandleCardAdded(item);
            // _cards.OnAdded += HandleCardAdded;

            _view.CancelButton.SetAction(CancelClick);
            _view.Animator.SwitchTo(Modes.Disabled.ToString());
            SetMode(Modes.Choose);
            // _ghostRepository.OnAdded += HandleGhostRepositoryShowed;
            // _ghostRepository.OnRemoved += HandleGhostRepositoryHided;
        }

        protected override void DisposeInner()
        {
            // _ghostRepository.OnAdded -= HandleGhostRepositoryShowed;
            // _ghostRepository.OnRemoved -= HandleGhostRepositoryHided;
            // _cards.OnAdded -= HandleCardAdded;
        }

        private void HandleCardAdded(ConstructionCardPresentation obj)
        {
            var view = _view.Cards.Spawn<IHandConstructionView>(_view.CardPrototype);
            view.Init(obj.Id);
        }

        private void CancelClick()
        {
            _screenService.Open<IMainScreenView>(x => x.Init());
        }

        private void HandleGhostRepositoryHided()
        {
            SetMode(Modes.Choose);
        }

        private void HandleGhostRepositoryShowed()
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
