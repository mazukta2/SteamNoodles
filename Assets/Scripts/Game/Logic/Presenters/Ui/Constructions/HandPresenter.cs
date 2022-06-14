using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        private readonly IPresenterRepository<ConstructionCard> _repository;
        private readonly BuildingModeService _buildingService;
        private readonly ScreenService _screenService;
        private readonly IHandView _view;

        public HandPresenter(IHandView view)
            : this(view,
                  IPresenterServices.Default?.Get<IPresenterRepository<ConstructionCard>>(),
                  IPresenterServices.Default?.Get<BuildingModeService>(),
                  IPresenterServices.Default?.Get<ScreenService>())
        {
        }

        public HandPresenter(IHandView view,
            IPresenterRepository<ConstructionCard> repository,
            BuildingModeService buildingService,
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
            _buildingService.OnChanged += HandleVisualModesChanged;
        }

        protected override void DisposeInner()
        {
            _buildingService.OnChanged -= HandleVisualModesChanged;
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

        private void HandleVisualModesChanged(bool state)
        {
            if (state)
                SetMode(Modes.Build);
            else
                SetMode(Modes.Choose);
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
