﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Common.Creation
{
    // prefab must create view presenter by request
    public abstract class MockViewPrefab : IViewPrefab
    {
        public abstract IView CreateView<T>(LevelView level, MockContainerView container) where T : IView;

        public IView CreateInContainer<T>(MockContainerView conteiner) where T : IView
        {
            return conteiner.Create((l) => CreateView<T>(l, conteiner));
        }

    }
}
