using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views;
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
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }
        }

        public PlacementCellPresenter CreateCell(IntPoint position)
        {
            return _view.Manager.Cell.Create<CellView>(_view.Manager.CellsContainer).Init(this, position, GetOffset());
        }

        private FloatPoint GetOffset()
        {
            var offset = FloatPoint.Zero;
            if (_model.Size.X % 2 == 0)
                offset += new FloatPoint(0.5f, 0);
            if (_model.Size.Y % 2 == 0)
                offset += new FloatPoint(0, 0.5f);

            return offset * _model.ConstructionsSettings.CellSize;
        }
    }
}
