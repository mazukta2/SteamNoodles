using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PlacementFieldView : PresenterView<PlacementFieldPresenter>, IPlacementFieldView
    {
        public IPlacementManagerView Manager { get; }
        public int Id { get; }

        public PlacementFieldView(LevelView level, PlacementManagerView managerView, int id) : base(level)
        {
            if (managerView == null) throw new ArgumentNullException(nameof(managerView));
            if (id < 0) throw new ArgumentNullException(nameof(id));
            if (id >= Level.Model.Constructions.Placements.Count) throw new ArgumentNullException(nameof(id));
            Id = id;
            Manager = managerView;
        }
    }
}