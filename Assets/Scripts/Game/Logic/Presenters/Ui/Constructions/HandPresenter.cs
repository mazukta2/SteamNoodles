using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        public static HandPresenter Default { get; set; }

        //public Modes Mode { get => _mode; set => SetMode(value); }
        private readonly IPresenterRepository<ConstructionCard> _repository;
        private readonly BuildingModeService _buildingService;
        private readonly IHandView _view;
        private Modes _mode;

        public HandPresenter(IHandView view) 
            : this(view, 
                  IStageLevelPresenterRepository.Default?.Cards, 
                  IPresenterServices.Default?.Get<BuildingModeService>())
        {
        }

        public HandPresenter(IHandView view, 
            IPresenterRepository<ConstructionCard> repository, BuildingModeService buildingService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));

            var cards = _repository.Get();
            foreach (var item in cards)
                HandleCardAdded(item, null);
            _repository.OnAdded += HandleCardAdded;

            _view.CancelButton.SetAction(CancelClick);
            _view.Animator.SwitchTo(Modes.Disabled.ToString());
            _buildingService.OnChanged += HandleVisualModesChanged;
        }

        protected override void DisposeInner()
        {
            _buildingService.OnChanged -= HandleVisualModesChanged;
            _repository.OnAdded -= HandleCardAdded;
        }

        private void HandleCardAdded(EntityLink<ConstructionCard> entity, ConstructionCard obj)
        {
            var view = _view.Cards.Spawn<IHandConstructionView>(_view.CardPrototype);
            new HandConstructionPresenter(entity, view);
        }

        private void CancelClick()
        {
            ScreenManagerPresenter.Default.GetCollection<CommonScreens>().Open<IMainScreenView>();
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
            _mode = value;
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
