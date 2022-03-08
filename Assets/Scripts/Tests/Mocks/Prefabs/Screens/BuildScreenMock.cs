using Game.Assets.Scripts.Game.Unity.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class BuildScreenMock : MockPrefab<BuildScreenView>
    {
        public override void Mock(List<IDisposable> disposables, BuildScreenView s)
        {
            var closeButton = new ButtonView();
            disposables.Add(closeButton);
            s.CloseButton = closeButton;
        }
    }
}
