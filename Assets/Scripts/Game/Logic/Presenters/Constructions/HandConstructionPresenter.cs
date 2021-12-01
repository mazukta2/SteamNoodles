using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class HandConstructionPresenter : IPresenter
    {
        public IHandConstructionView View => _view;
        public bool IsDestoyed { get; private set; }
        public ConstructionScheme Scheme => _model;

        private ConstructionScheme _model;
        private IHandConstructionView _view;
        private Action<ConstructionScheme> _onClick;

        public HandConstructionPresenter(ConstructionScheme model, IHandConstructionView view, Action<ConstructionScheme> onClick)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;
            _onClick = onClick;

            UpdateView();
        }

        public void Destroy()
        {
            IsDestoyed = true;
            View.Destroy();
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
