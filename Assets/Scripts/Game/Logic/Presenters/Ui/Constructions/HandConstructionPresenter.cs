using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter<IHandConstructionView>
    {
        private PresenterModel<ConstructionCard> _link;
        private IHandConstructionView _view;
        private ScreenManagerPresenter _screenManager;
        private readonly IPresenterRepository<Construction> _constructions;
        private HandConstructionsAnimations _animations;
        private CardAmount _currentAmount;

        public HandConstructionPresenter(EntityLink<ConstructionCard> model, ScreenManagerPresenter screenManager, IPresenterRepository<Construction> constructions,
            IHandConstructionView view) : base(view)
        {
            _link = model.CreateModel() ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _animations = new HandConstructionsAnimations(view);

            view.Button.SetAction(HandleClick);

            _link.OnChanged += HandleOnChanged;
            _link.OnRemoved += Model_OnDispose;
            _view.OnHighlihgtedEnter += _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit += _view_OnHighlihgtedExit;
            _animations.OnAnimationsCompleted += _animations_OnAnimationsCompleted;

            _view.Image.SetPath(_link.Get().HandImagePath);
            UpdateAmount();
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _link.Dispose();
            _animations.Dispose();
            _view.TooltipContainer.Clear();
            _link.OnChanged -= HandleOnChanged;
            _view.OnHighlihgtedEnter -= _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit -= _view_OnHighlihgtedExit;
            _link.OnRemoved -= Model_OnDispose;
        }

        private void HandleClick()
        {
            _screenManager.GetCollection<BuildScreenCollection>().Open(_link.Get());
        }

        private void UpdateAmount()
        {
            var amount = _link.Get().Amount.Value;
            if (_currentAmount == null)
                _animations.Add(amount);
            else
            {
                if (_currentAmount.Value < amount)
                    _animations.Add(amount - _currentAmount.Value);
                else if (_currentAmount.Value > amount)
                    _animations.Remove(_currentAmount.Value - amount);
            }

            _currentAmount = _link.Get().Amount;

        }

        private void HandleOnChanged()
        {
            UpdateAmount();
        }

        private void Model_OnDispose()
        {
            _view.Button.IsActive = false;
            _animations.Destroy();
        }

        private void _animations_OnAnimationsCompleted()
        {
            if (_link.IsRemoved())
                _view.Dispose();
        }

        private void _view_OnHighlihgtedEnter()
        {
            _view.TooltipContainer.Clear();
            var tooltip = _view.TooltipContainer.Spawn<IHandConstructionTooltipView>(_view.TooltipPrefab);
            new HandConstructionTooltipPresenter(tooltip, _constructions, _link.Get());
        }

        private void _view_OnHighlihgtedExit()
        {
            _view.TooltipContainer.Clear();
        }

    }
}
