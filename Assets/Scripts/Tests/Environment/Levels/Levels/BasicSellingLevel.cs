using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Environment;

namespace Game.Assets.Scripts.Tests.Mocks.Levels
{
    public class BasicSellingLevel : LevelMockCreator
    {
        public override void FillLevel(LevelInTests level)
        {
            var screenSpawnPoint = level.Add(new ViewContainer());
            level.Add(new ScreenManagerView()
            {
                Screen = screenSpawnPoint
            });
        }
    }
}
