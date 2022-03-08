using Game.Assets.Scripts.Game.Unity.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenMock : MockPrefab<MainScreenView>
    {
        public override void Spawn(LevelInTests level, MainScreenView s)
        {
            var buildButton = level.Add(new ButtonView());
            s.BuildButton = buildButton;
        }
    }
}
