using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens;
using Game.Tests.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Definitions.List
{
    public class DefaultDefinitions : DefinitionsMockCreator
    {
        public override void Create(GameEngineInTests engine)
        {
            engine.Assets.Screens.AddPrototype(new MainScreenMock());
        }
    }
}
