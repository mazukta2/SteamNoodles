using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class PlacementFieldView : View
    {
        public PlacementManagerView Manager;
        private PlacementFieldPresenter _presenter;

        public PlacementFieldView(ILevel level, PlacementManagerView managerView, int id) : base(level)
        {
            if (managerView == null) throw new ArgumentNullException(nameof(managerView));
            if (id < 0) throw new ArgumentNullException(nameof(id));
            if (id >= Level.Model.Constructions.Placements.Count) throw new ArgumentNullException(nameof(id));

            Manager = managerView;

            var field = Level.Model.Constructions.Placements.ElementAt(id);
            _presenter = new PlacementFieldPresenter(field, this, managerView.Presenter);
        }

    }
}