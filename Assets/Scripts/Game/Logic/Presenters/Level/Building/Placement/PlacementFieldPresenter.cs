using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter<IPlacementFieldView>
    {
        private IPlacementFieldView _view;
        private readonly IFieldModel _field;

        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(IPlacementFieldView view) : this(view,
            IPresenterServices.Default.Get<FieldRequestsService>().Get())
        {
        }

        public PlacementFieldPresenter(IPlacementFieldView view, IFieldModel field) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _field = field ?? throw new ArgumentNullException(nameof(field));

            var boundaries = _field.Boudaries;
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }

            _field.OnUpdate += UpdateCells;
            _field.OnConstructionBuilded += HandleOnConstructionBuilded;
            UpdateCells();
        }

        protected override void DisposeInner()
        {
            _field.OnUpdate -= UpdateCells;
            _field.OnConstructionBuilded -= HandleOnConstructionBuilded;
            _field.Dispose();
        }

        private PlacementCellPresenter CreateCell(IntPoint position)
        {
            var view = _view.CellsContainer.Spawn<ICellView>(_view.Cell);
            return new PlacementCellPresenter(view, position, _field);
        }

        private void UpdateCells()
        {
            foreach (var cell in _cells)
            {
                cell.SetState(_field.GetStatus(cell.Position));
            }
        }

        private void HandleOnConstructionBuilded(Uid id)
        {
            var view = _view.ConstrcutionContainer.Spawn<IConstructionView>(_view.ConstrcutionPrototype);
            _field.CreatePresenter(view, id);
        }
    }
}
