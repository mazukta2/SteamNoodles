using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Level.Units;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels
{
    public class BasicSellingLevel : ViewCollectionPrefabMock
    {
        public override void Fill(IViewsCollection collection)
        {
            new ScreenManagerView(collection);

            new GhostManagerView(collection);

            new PlacementFieldView(collection);

            new UnitsManagerView(collection);

            new PieceSpawnerView(collection);

            new PointCounterWidgetView(collection);

            new HandView(collection);

            new BuildingTooltipMock(collection);
        }


        private class UnitViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViewsCollection collection)
            {
                new UnitView(collection);
            }
        }
    }
}
