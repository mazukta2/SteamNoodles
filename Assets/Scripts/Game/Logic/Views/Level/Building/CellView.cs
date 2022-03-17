using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class CellView : View
    {
        public FloatPoint LocalPosition { get; set; }
        public CellPlacementStatus State { get; internal set; }
        public PlacementCellPresenter Presenter { get; private set; }

        public CellView(ILevel level) : base(level)
        {

        }

        public PlacementCellPresenter Init(PlacementFieldPresenter field, IntPoint point, FloatPoint offset)
        {
            Presenter = new PlacementCellPresenter(this, field, Level.Engine.Definitions.Get<ConstructionsSettingsDefinition>(), point, offset);
            return Presenter;
        }
    }
}