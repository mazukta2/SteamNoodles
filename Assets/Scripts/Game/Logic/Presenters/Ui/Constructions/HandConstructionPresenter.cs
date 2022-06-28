using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions.Animations;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter<IHandConstructionView>
    {
        private IHandConstructionView _view;
        private readonly ScreenService _screenService;
        private HandConstructionsAnimations _animations;
        private CardAmount _currentAmount;
        private bool _isModelDisposed;
        private readonly IDataProvider<ConstructionCardData> _model;

        public HandConstructionPresenter(IHandConstructionView view, IDataProvider<ConstructionCardData> model) 
            : this(view, model, 
                  IPresenterServices.Default.Get<ScreenService>())
        {

        }

        public HandConstructionPresenter(IHandConstructionView view, IDataProvider<ConstructionCardData> model,
             ScreenService screenService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _screenService = screenService ?? throw new ArgumentNullException(nameof(screenService));
            _animations = new HandConstructionsAnimations(view);

            view.Button.SetAction(HandleClick);

            _model.OnEvent += HandleOnEvent;
            _model.OnRemoved += Model_OnDispose;
            _view.OnHighlihgtedEnter += _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit += _view_OnHighlihgtedExit;
            _animations.OnAnimationsCompleted += _animations_OnAnimationsCompleted;

            _view.Image.SetPath(_model.Get().HandImagePath);
            UpdateAmount();
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _animations.Dispose();
            _view.TooltipContainer.Clear();
            _model.OnEvent -= HandleOnEvent;
            _view.OnHighlihgtedEnter -= _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit -= _view_OnHighlihgtedExit;
            _model.OnRemoved -= Model_OnDispose;
        }

        private void HandleClick()
        {
            _screenService.Open<IBuildScreenView>(view => view.Init(_model.Get().Id));
        }

        private void UpdateAmount()
        {
            var amount = _model.Get().Amount.Value;
            if (_currentAmount == null)
                _animations.Add(amount);
            else
            {
                if (_currentAmount.Value < amount)
                    _animations.Add(amount - _currentAmount.Value);
                else if (_currentAmount.Value > amount)
                    _animations.Remove(_currentAmount.Value - amount);
            }

            _currentAmount = _model.Get().Amount;

        }

        private void HandleOnEvent(IModelEvent e)
        {
            if (e is not HandConstructionAmountChangedEvent)
                return;
            
            UpdateAmount();
        }

        private void Model_OnDispose()
        {
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
            view.Init(_model.Get().Id);
        }

        private void _view_OnHighlihgtedExit()
        {
            _view.TooltipContainer.Clear();
        }

    }
}
