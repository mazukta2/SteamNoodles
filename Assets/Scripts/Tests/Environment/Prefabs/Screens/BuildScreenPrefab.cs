﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using Game.Assets.Scripts.Tests.Environment.Views.Ui.Screens;

namespace Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens
{
    public class BuildScreenPrefab : MockViewPrefab
    {
        public override IView CreateView<T>(LevelView level, MockContainerView container)
        {
            return new BuildScreenView(level, new ButtonMock(), new UiText(), new UiText(), new ProgressBar());
        }
    }
}
