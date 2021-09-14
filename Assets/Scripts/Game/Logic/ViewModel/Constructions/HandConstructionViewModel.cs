using Assets.Scripts.Models.Buildings;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class HandConstructionViewModel
    {
        public IHandConstructionView View => _view;

        private ConstructionScheme _model;
        private IHandConstructionView _view;
        private Action<ConstructionScheme> _onClick;

        public HandConstructionViewModel(ConstructionScheme model, IHandConstructionView view, Action<ConstructionScheme> onClick)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;
            _onClick = onClick;

            UpdateView();
        }

        public void OnClick()
        {
            _onClick(_model);
        }

        private void UpdateView()
        {
            _view.SetIcon(_model.HandIcon);
        }

    }
}
