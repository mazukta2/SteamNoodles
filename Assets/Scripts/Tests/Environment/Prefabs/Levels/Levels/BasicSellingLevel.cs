using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Environment.Common;

namespace Game.Assets.Scripts.Tests.Mocks.Levels
{
    public class BasicSellingLevel : LevelPrefabMock
    {
        public override void FillLevel(LevelInTests level)
        {
            var screenSpawnPoint = new ContainerView(level);
            new ScreenManagerView(level, screenSpawnPoint);

            var ghostContainer = new ContainerView(level);
            var ghostPrototype = new PrototypeView(level, new GhostViewPrefab());
            new GhostManagerView(level, ghostContainer, ghostPrototype);

            var cellContainer = new ContainerView(level);
            var cellPrototype = new PrototypeView(level, new CellViewPrefab());
            var constrcutionContainer = new ContainerView(level);
            var constructionPrototype = new PrototypeView(level, new ConstructionViewPrefab());
            var placementManager = new PlacementManagerView(level, cellContainer, cellPrototype, constrcutionContainer, constructionPrototype);

            new PlacementFieldView(level, placementManager, 0);

            new UnitsManagerView(level, new ContainerView(level), new PrototypeView(level, new UnitViewPrefab()));
        }

        private class GhostViewPrefab : ViewPrefab
        {
            public override View Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    var contrainer = new ContainerView(level);
                    return new GhostView(level, contrainer, new LevelPosition());
                });
            }
        }

        private class CellViewPrefab : ViewPrefab
        {
            public override View Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    return new CellView(level, new LevelPosition());
                });
            }
        }

        private class ConstructionViewPrefab : ViewPrefab
        {
            public override View Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    var c = new ContainerView(level);
                    return new ConstructionView(level,c, new LevelPosition());
                });
            }
        }

        private class UnitViewPrefab : ViewPrefab
        {
            public override View Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    return new UnitView(level);
                });
            }
        }
    }
}
