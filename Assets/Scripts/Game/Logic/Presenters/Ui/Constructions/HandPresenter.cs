using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        private readonly IPresenterRepository<ConstructionCard> _repository;
        private readonly BuildingModeService _buildingService;
        private readonly IHandView _view;

        public HandPresenter(IHandView view)
            : this(view,
                  IPresenterServices.Default?.Get<IPresenterRepository<ConstructionCard>>(),
                  IPresenterServices.Default?.Get<BuildingModeService>())
        {
        }

        public HandPresenter(IHandView view,
            IPresenterRepository<ConstructionCard> repository,
            BuildingModeService buildingService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));

            var cards = _repository.Get();
            foreach (var item in cards)
                HandleCardAdded(item, null);
            _repository.OnAdded += HandleCardAdded;
            _repository.OnRemoved += HandleCardRemoved;

            _view.CancelButton.SetAction(CancelClick);
            _view.Animator.SwitchTo(Modes.Disabled.ToString());
            SetMode(Modes.Choose);
            _buildingService.OnChanged += HandleVisualModesChanged;
        }

        protected override void DisposeInner()
        {
            _buildingService.OnChanged -= HandleVisualModesChanged;
            _repository.OnAdded -= HandleCardAdded;
            _repository.OnRemoved += HandleCardRemoved;
        }

        private void HandleCardAdded(EntityLink<ConstructionCard> entity, ConstructionCard obj)
        {
            var view = _view.Cards.Spawn<IHandConstructionView>(_view.CardPrototype);
            //_commands.Execute(new AddHandConstructionCommand(entity, _view.Cards, _view.CardPrototype));
        }

        private void HandleCardRemoved(EntityLink<ConstructionCard> entity, ConstructionCard obj)
        {
            //_commands.Execute(new RemoveHandConstructionCommand(_view.Cards));
        }

        private void CancelClick()
        {
            //ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
            //_commands.Execute(new OpenMainScreenCommand());
            //_commands.Execute(new OpenMainScreenCommand());
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
