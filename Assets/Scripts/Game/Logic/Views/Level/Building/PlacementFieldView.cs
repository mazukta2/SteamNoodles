using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class PlacementFieldView : View
    {
        private PlacementFieldPresenter _presenter;

        public PlacementFieldView(ILevel level, PlacementManagerView managerView) : base(level)
        {
            if (managerView == null) throw new ArgumentNullException(nameof(managerView));

            _presenter = new PlacementFieldPresenter(this, managerView.Presenter);
        }
    }
}