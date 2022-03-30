using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Common.Creation
{
    // prefab must create view presenter by request
    public abstract class TestViewPrefab : IViewPrefab
    {
        public abstract View Create<T>(TestContainerView conteiner) where T : View;
    }
}
