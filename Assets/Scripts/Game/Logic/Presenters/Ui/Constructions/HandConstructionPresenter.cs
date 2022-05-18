using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter<IHandConstructionView>
    {
        private ConstructionCard _model;
        private IHandConstructionView _view;
        private ScreenManagerPresenter _screenManager;
        private PlacementField _field;
        private HandConstructionsAnimations _animations;

        public HandConstructionPresenter(ScreenManagerPresenter screenManager, 
            IHandConstructionView view, ConstructionCard model, PlacementField field, PlayerHand.ConstructionSource source) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _animations = new HandConstructionsAnimations(view);

            view.Button.SetAction(HandleClick);

            _model.OnAdded += _model_OnAdded;
            _model.OnRemoved += _model_OnRemoved;
            _model.OnDispose += Model_OnDispose;
            _view.OnHighlihgtedEnter += _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit += _view_OnHighlihgtedExit;
            UpdateView();

            _animations.Add(_model.Amount);

            _animations.OnAnimationsCompleted += _animations_OnAnimationsCompleted;
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _animations.Dispose();
            _view.TooltipContainer.Clear();
            _model.OnAdded -= _model_OnAdded;
            _model.OnRemoved -= _model_OnRemoved;
            _model.OnDispose -= Model_OnDispose;
            _view.OnHighlihgtedEnter -= _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit -= _view_OnHighlihgtedExit;
        }

        private void _model_OnAdded(int obj, PlayerHand.ConstructionSource source)
        {
            _animations.Add(obj);
            UpdateView();
        }

        private void _model_OnRemoved(int obj)
        {
            _animations.Remove(obj);
            UpdateView();
        }

        private void HandleClick()
        {
            _screenManager.GetCollection<BuildScreenCollection>().Open(_model);
        }

        private void UpdateView()
        {
            _view.Image.SetPath(_model.Definition.HandImagePath);
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
            var tooltip = _view.TooltipContainer.Spawn<IHandConstructionTooltipView>(_view.TooltipPrefab);
            new HandConstructionTooltipPresenter(tooltip, _model, _field);
        }

        private void _view_OnHighlihgtedExit()
        {
            _view.TooltipContainer.Clear();
        }

    }
}
