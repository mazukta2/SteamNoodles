using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class PlacementFieldPresenter : BasePresenter
    {
        private PlacementFieldView _view;
        private PlacementManagerPresenter _manager;

        public PlacementFieldPresenter(PlacementFieldView view, PlacementManagerPresenter presenter) : base(view)
        {
            _view = view;
            _manager = presenter;
        }
    }
}
