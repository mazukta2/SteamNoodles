using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementCellPresenter : BasePresenter<ICellView>
    {
        public IntPoint Position => _position;

        private ICellView _cellView;
        private PlacementFieldPresenter _field;
        private IntPoint _position;
        private ConstructionsSettingsDefinition _constructionsSettingsDefinition;

        public PlacementCellPresenter(ICellView view, PlacementFieldPresenter field, 
            ConstructionsSettingsDefinition constructionsSettingsDefinition, IntPoint position) : base(view)
        {
            _cellView = view ?? throw new ArgumentNullException(nameof(view));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _position = position;
            _constructionsSettingsDefinition = constructionsSettingsDefinition;

            view.LocalPosition.Value = GetPosition();
        }

        private FloatPoint GetPosition() => _field.GetLocalPosition(_position);

        public void SetState(CellPlacementStatus state)
        {
            _cellView.State.Value = state;
        }
    }
}
