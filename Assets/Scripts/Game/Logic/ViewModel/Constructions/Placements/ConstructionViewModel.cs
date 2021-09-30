using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.ViewModel;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class ConstructionViewModel : IViewModel
    {
        private Construction _model;
        public IConstructionView View { get; private set; }

        public bool IsDestoyed { get; set; }

        public ConstructionViewModel(PlacementViewModel placement, Construction construction, IConstructionView view)
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
