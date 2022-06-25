using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions.Animations;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter<IHandConstructionView>
    {
        private ConstructionCard _model;
        private IHandConstructionView _view;
        private readonly IQuery<ConstructionCard> _cards;
        private readonly ScreenService _screenService;
        private HandConstructionsAnimations _animations;
        private CardAmount _currentAmount;
        private bool _isModelDisposed;

        public HandConstructionPresenter(IHandConstructionView view, ConstructionCard model) 
            : this(view, model, 
                  IPresenterServices.Default.Get<IRepository<ConstructionCard>>().AsQuery(),
                  IPresenterServices.Default.Get<ScreenService>())
        {

        }

        public HandConstructionPresenter(IHandConstructionView view, ConstructionCard model,
             IQuery<ConstructionCard> cards,
             ScreenService screenService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _cards = cards ?? throw new ArgumentNullException(nameof(cards));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));
            _animations = new HandConstructionsAnimations(view);

            view.Button.SetAction(HandleClick);

            _cards.OnEvent += HandleOnEvent;
            _cards.OnRemoved += Model_OnDispose;
            _view.OnHighlihgtedEnter += _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit += _view_OnHighlihgtedExit;
            _animations.OnAnimationsCompleted += _animations_OnAnimationsCompleted;

            _view.Image.SetPath(_model.HandImagePath);
            UpdateAmount();
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _cards.Dispose();
            _animations.Dispose();
            _view.TooltipContainer.Clear();
            _cards.OnEvent -= HandleOnEvent;
            _view.OnHighlihgtedEnter -= _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit -= _view_OnHighlihgtedExit;
            _cards.OnRemoved -= Model_OnDispose;
        }

        private void HandleClick()
        {
            _screenService.Open<IBuildScreenView>(view => view.Init(_model));
        }

        private void UpdateAmount()
        {
            var amount = _model.Amount.Value;
            if (_currentAmount == null)
                _animations.Add(amount);
            else
            {
                if (_currentAmount.Value < amount)
                    _animations.Add(amount - _currentAmount.Value);
                else if (_currentAmount.Value > amount)
                    _animations.Remove(_currentAmount.Value - amount);
            }

            _currentAmount = _model.Amount;

        }

        private void HandleOnEvent(ConstructionCard card, IModelEvent e)
        {
            if (e is not HandConstructionAmountChangedEvent)
                return;
            
            if (card.Id != _model.Id)
                return;

            _model = card;

            UpdateAmount();
        }

        private void Model_OnDispose(ConstructionCard card)
        {
            if (card.Id != _model.Id)
                return;

            _isModelDisposed = true;
            _view.Button.IsActive = false;
            _animations.Destroy();
        }

        private void _animations_OnAnimationsCompleted()
        {
            if (_isModelDisposed)
                _view.Dispose();
        }

        private void _view_OnHighlihgtedEnter()
        {
            _view.TooltipContainer.Clear();
            var view = _view.TooltipContainer.Spawn<IHandConstructionTooltipView>(_view.TooltipPrefab);
            view.Init(_model);
        }

        private void _view_OnHighlihgtedExit()
        {
            _view.TooltipContainer.Clear();
        }

    }
}
