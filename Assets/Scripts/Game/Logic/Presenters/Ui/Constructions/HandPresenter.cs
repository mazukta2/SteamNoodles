using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
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
        //private readonly IPresenterRepository<ConstructionCard> _repository;
        //private readonly BuildingModeService _buildingService;
        private readonly IHandView _view;
        private readonly IHandModel _model;
        private Modes _mode;

        public HandPresenter(IHandView view) 
            : this(view, 
                  IPresenterServices.Default?.Get<HandRequestsService>().Get())
        {
        }

        public HandPresenter(IHandView view, IHandModel model) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));

            var cards = _model.GetCards();
            foreach (var item in cards)
                HandleCardAdded(item);

            _model.OnAdded += HandleCardAdded;

            _view.CancelButton.SetAction(CancelClick);
            _view.Animator.SwitchTo(Modes.Disabled.ToString());
            SetMode(Modes.Choose);
            //_buildingService.OnChanged += HandleVisualModesChanged;
        }

        protected override void DisposeInner()
        {
            _model.OnAdded -= HandleCardAdded;

            //_buildingService.OnChanged -= HandleVisualModesChanged;
            //_repository.OnAdded -= HandleCardAdded;
        }

        private void HandleCardAdded(IConstructionHandModel model)
        {
            var view = _view.Cards.Spawn<IHandConstructionView>(_view.CardPrototype);
            model.ConnectPresenter(view);
        }

        private void CancelClick()
        {
            //ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
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
