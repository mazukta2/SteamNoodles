using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Common.Creation
{
    // prefab must create view presenter by request
    public abstract class TestViewPrefab : IViewPrefab
    {
        public abstract View CreateView<T>(ILevel level, TestContainerView container) where T : View;

        public View CreateInContainer<T>(TestContainerView conteiner) where T : View
        {
            return conteiner.Create((l) => CreateView<T>(l, conteiner));
        }

    }
}
