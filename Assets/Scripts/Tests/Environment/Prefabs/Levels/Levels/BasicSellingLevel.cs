using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment;

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
        }

        private class GhostViewPrefab : ViewPrefab
        {
            public override object Create<T>(ContainerView conteiner)
            {
                return conteiner.Create((level) =>
                {
                    return new GhostView(level);
                });
            }
        }
    }
}
