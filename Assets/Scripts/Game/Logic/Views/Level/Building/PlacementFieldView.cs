using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class PlacementFieldView : PresenterView<PlacementFieldPresenter>
    {
        public PlacementManagerView Manager;
        private ServiceWaiter<GhostManagerService> _wait;
        private int _id;

        public PlacementFieldView(ILevel level, PlacementManagerView managerView, int id) : base(level)
        {
            if (managerView == null) throw new ArgumentNullException(nameof(managerView));
            if (id < 0) throw new ArgumentNullException(nameof(id));
            if (id >= Level.Model.Constructions.Placements.Count) throw new ArgumentNullException(nameof(id));
            _id = id;

            Manager = managerView;

            _wait = Level.Services.Request<GhostManagerService>(Init);
        }

        protected override void DisposeInner()
        {
            _wait.Dispose();
        }

        private void Init(GhostManagerService ghost)
        {
            var field = Level.Model.Constructions.Placements.ElementAt(_id);
            new PlacementFieldPresenter(ghost.Get(), field, this, 
                Level.Engine.Definitions.Get<ConstructionsSettingsDefinition>(), 
                Manager.Presenter, Level.Engine.Assets);
        }
    }
}