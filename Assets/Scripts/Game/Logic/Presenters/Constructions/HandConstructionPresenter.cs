using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class HandConstructionPresenter : Disposable
    {
        public IHandConstructionView View => _view;
        public ConstructionCard Scheme => _model;

        private ConstructionCard _model;
        private IHandConstructionView _view;
        private Action<ConstructionCard> _onClick;

        public HandConstructionPresenter(ConstructionCard model, IHandConstructionView view, Action<ConstructionCard> onClick)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;
            _onClick = onClick;

            UpdateView();
        }

        protected override void DisposeInner()
        {
            View.Dispose();
        }

        private void OnClick()
        {
            _onClick(_model);
        }

        private void UpdateView()
        {
            _view.SetIcon(_model.HandIcon);
            _view.SetClick(OnClick);
        }
    }
}
