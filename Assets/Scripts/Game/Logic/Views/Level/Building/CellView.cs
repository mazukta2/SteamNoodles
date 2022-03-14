using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class CellView : View
    {
        private CellPresenter _presenter;

        public CellView(ILevel level) : base(level)
        {

        }

        public void Init(PlacementFieldPresenter field)
        {
            _presenter = new CellPresenter(this, field);
        }
    }
}