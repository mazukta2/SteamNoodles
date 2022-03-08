using Game.Assets.Scripts.Game.Unity.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class MainScreenMock : MockPrefab<MainScreenView>
    {
        public override void Mock(List<IDisposable> disposables, MainScreenView s)
        {
            var buildButton = new ButtonView();
            disposables.Add(buildButton);
            s.BuildButton = buildButton;
        }
    }
}
