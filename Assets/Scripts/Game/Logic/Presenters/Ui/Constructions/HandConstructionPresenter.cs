﻿using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter<IHandConstructionView>
    {
        private PresenterModel<ConstructionCard> _model;
        private IHandConstructionView _view;
        private HandConstructionsAnimations _animations;
        private CardAmount _currentAmount;
        private Action<IHandConstructionTooltipView> _fillTooltip;

        public HandConstructionPresenter(EntityLink<ConstructionCard> model,
            IHandConstructionView view, Action<IHandConstructionTooltipView> fillTooltip) : this(model, view)
        {
            _fillTooltip = fillTooltip ?? throw new ArgumentNullException(nameof(fillTooltip));
        }

        public HandConstructionPresenter(EntityLink<ConstructionCard> model,
            IHandConstructionView view) : base(view)
        {
            _model = model.CreateModel() ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _animations = new HandConstructionsAnimations(view);
            _fillTooltip = FillTooltipPresenter;

            view.Button.SetAction(HandleClick);

            _model.OnChanged += HandleOnChanged;
            _model.OnDispose += Model_OnDispose;
            _view.OnHighlihgtedEnter += _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit += _view_OnHighlihgtedExit;
            _animations.OnAnimationsCompleted += _animations_OnAnimationsCompleted;

            _view.Image.SetPath(_model.Get().HandImagePath);
            UpdateAmount();
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _model.Dispose();
            _animations.Dispose();
            _view.TooltipContainer.Clear();
            _model.OnChanged -= HandleOnChanged;
            _view.OnHighlihgtedEnter -= _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit -= _view_OnHighlihgtedExit;
            _model.OnDispose -= Model_OnDispose;
        }

        private void HandleClick()
        {
            ScreenManagerPresenter.Default.GetCollection<BuildScreenCollection>().Open(_model.GetLink());
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
            if (_model.IsDisposed)
                _view.Dispose();
        }

        private void _view_OnHighlihgtedEnter()
        {
            _view.TooltipContainer.Clear();
            var view = _view.TooltipContainer.Spawn<IHandConstructionTooltipView>(_view.TooltipPrefab);
            _fillTooltip(view);
        }

        private void _view_OnHighlihgtedExit()
        {
            _view.TooltipContainer.Clear();
        }

        private void FillTooltipPresenter(IHandConstructionTooltipView view)
        {
            new HandConstructionTooltipPresenter(view).SetModel(_model.GetLink());
        }
    }
}
