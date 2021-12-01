using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : IPresenter
    {
        private Construction _model;
        public IConstructionView View { get; private set; }

        public bool IsDestoyed { get; set; }

        public ConstructionPresenter(PlacementPresenter placement, Construction construction, IConstructionView view)
        {
            _model = construction;
            View = view;
            View.SetPosition(placement.GetWorldPosition(construction.Position));
            View.SetImage(_model.GetVisual());
        }

        public void Destroy()
        {
            IsDestoyed = true;
            View.Destroy();
        }
    }
}
