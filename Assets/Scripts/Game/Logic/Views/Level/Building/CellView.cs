using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class CellView : View
    {
        public FloatPoint LocalPosition { get; set; }
        public PlacementCellPresenter Presenter { get; private set; }

        public CellView(ILevel level) : base(level)
        {

        }

        public PlacementCellPresenter Init(PlacementFieldPresenter field)
        {
            Presenter = new PlacementCellPresenter(this, field);
            return Presenter;
        }
    }
}