using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class ConstructionViewModel
    {
        private Construction _model;
        public IConstructionView View { get; private set; }

        public ConstructionViewModel(PlacementViewModel placement, Construction construction, IConstructionView view)
        {
            _model = construction;
            View = view;
            View.SetPosition(placement.GetWorldPosition(construction.Position));
            View.SetImage(_model.GetVisual());
        }
    }
}
