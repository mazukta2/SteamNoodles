using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : Disposable
    {
        private Construction _model;
        public IConstructionView View { get; private set; }

        public ConstructionPresenter(PlacementPresenter placement, Construction construction, IConstructionView view)
        {
            _model = construction;
            View = view;
            View.SetPosition(placement.GetWorldPosition(construction.Position));
            View.SetImage(_model.GetVisual());
        }

        protected override void DisposeInner()
        {
            View.Dispose();
        }
    }
}
