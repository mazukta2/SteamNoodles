using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Building;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        private readonly IPresenterRepository<ConstructionCard> _repository;
        private readonly BuildingModeService _buildingService;
        private readonly ICommands _commands;
        private readonly IHandView _view;
        private Modes _mode;

        public HandPresenter(IHandView view) 
            : this(view, 
                  IPresenterServices.Default?.Get<IPresenterRepository<ConstructionCard>>(),
                  IPresenterServices.Default?.Get<BuildingModeService>(),
                  ICommands.Default)
        {
        }

        public HandPresenter(IHandView view, 
            IPresenterRepository<ConstructionCard> repository, 
            BuildingModeService buildingService,
            ICommands commands) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            _commands = commands ?? throw new ArgumentNullException(nameof(commands));

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
            _commands.Execute(new AddHandConstructionCommand(entity, _view.Cards, _view.CardPrototype));
        }

        private void HandleCardRemoved(EntityLink<ConstructionCard> entity, ConstructionCard obj)
        {
            _commands.Execute(new RemoveHandConstructionCommand(_view.Cards));
        }

        private void CancelClick()
        {
            _commands.Execute(new OpenMainScreenCommand());
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
