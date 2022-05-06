using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.BuildScreenPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter<IHandConstructionView>
    {
        private ConstructionCard _model;
        private IHandConstructionView _view;
        private ScreenManagerPresenter _screenManager;
        private PlacementField _field;

        public HandConstructionPresenter(ScreenManagerPresenter screenManager, 
            IHandConstructionView view, ConstructionCard model, PlacementField field) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _field = field ?? throw new ArgumentNullException(nameof(field));

            view.Button.SetAction(HandleClick);

            _model.OnDispose += Model_OnDispose;
            _view.OnHighlihgtedEnter += _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit += _view_OnHighlihgtedExit;
            UpdateView();
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _view.TooltipContainer.Clear();
            _model.OnDispose -= Model_OnDispose;
            _view.OnHighlihgtedEnter -= _view_OnHighlihgtedEnter;
            _view.OnHighlihgtedExit -= _view_OnHighlihgtedExit;
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
