using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Tests.Mocks.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Levels.Resources
{
    public class LevelResourcesView : TestView, ILevelResourcesView
    {
        public IIntValueView Money { get; } = new IntValueView();

        protected override void DisposeInner()
        {
            Money.Dispose();
        }
    }
}
