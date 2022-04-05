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
    public class CellView : PresenterView<PlacementCellPresenter>
    {
        public event Action OnUpdate = delegate { };
        public ILevelPosition LocalPosition { get; private set; }
        public ISwitcher<CellPlacementStatus> State { get; private set; }

        public CellView(ILevel level, ISwitcher<CellPlacementStatus> state, ILevelPosition position) : base(level)
        {
            State = state;
            LocalPosition = position;
        }
    }
}