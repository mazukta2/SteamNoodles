using Game.Assets.Scripts.Game.Logic.Models.Constructions;
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

        public HandConstructionPresenter(ScreenManagerPresenter screenManager, IHandConstructionView view, ConstructionCard model) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            view.Button.SetAction(HandleClick);

            _model.OnDispose += Model_OnDispose;
            UpdateView();
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _model.OnDispose -= Model_OnDispose;
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

    }
}
