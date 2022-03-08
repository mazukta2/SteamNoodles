using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Game.ConstructorSettings
{
    public class DefaultPreset : BaseConstructorSettings
    {
        public override void Fill(GameTestConstructor constructor)
        {
            constructor
                .LoadDefinitions(new DefaultDefinitions())
                .AddAndLoadLevel(new BasicSellingLevel());
        }
    }
}
