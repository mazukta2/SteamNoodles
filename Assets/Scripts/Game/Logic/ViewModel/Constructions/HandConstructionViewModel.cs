using Assets.Scripts.Models.Buildings;
using System;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class HandConstructionViewModel
    {
        private ConstructionScheme _model;
        private Action<ConstructionScheme> _onClick;

        public HandConstructionViewModel(ConstructionScheme model, Action<ConstructionScheme> onClick)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            _onClick = onClick;
        }

        public void OnClick()
        {
            _onClick(_model);
        }
    }
}
