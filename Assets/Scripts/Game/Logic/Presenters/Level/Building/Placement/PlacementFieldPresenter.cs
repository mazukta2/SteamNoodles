using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class PlacementFieldPresenter : BasePresenter
    {
        private PlacementField _model;
        private PlacementFieldView _view;
        private PlacementManagerPresenter _manager;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(PlacementField model, PlacementFieldView view, PlacementManagerPresenter presenter) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _manager = presenter ?? throw new ArgumentNullException(nameof(presenter));

            for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            {
                for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
                {
                    _cells.Add(view.CreateCell(new IntPoint(x, y)));
                }
            }
        }
    }
}
