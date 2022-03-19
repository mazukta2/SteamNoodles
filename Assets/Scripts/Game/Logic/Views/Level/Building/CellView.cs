using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class CellView : View
    {
        private CellPlacementStatus _state;

        public event Action OnUpdate = delegate { };
        public ILevelPosition LocalPosition { get; private set; }
        public CellPlacementStatus State
        {
            get => _state; set
            {
                _state = value; OnUpdate();
            }
        }
        public PlacementCellPresenter Presenter { get; private set; }

        public CellView(ILevel level, ILevelPosition position) : base(level)
        {
            LocalPosition = position;
        }

        public PlacementCellPresenter Init(PlacementFieldPresenter field, IntPoint point)
        {
            Presenter = new PlacementCellPresenter(this, field, Level.Engine.Definitions.Get<ConstructionsSettingsDefinition>(), point);
            return Presenter;
        }
    }
}