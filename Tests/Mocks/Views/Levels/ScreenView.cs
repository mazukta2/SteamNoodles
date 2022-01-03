using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using Game.Tests.Mocks.Views.Clashes;
using Game.Tests.Mocks.Views.Levels.Resources;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Tests.Mocks.Views.Common;
using Tests.Tests.Mocks.Views.Levels;

namespace Game.Tests.Mocks.Views.Levels
{
    public class ScreenView : TestView, IScreenView
    {
        public IClashesView Clashes { get; } = new ClashesView();
        public IHandView Hand { get; } = new BasicHandView();
        public ICurrentOrderView Customers { get; } = new BasicCurrentOrderView();
        public ILevelResourcesView Resources { get; } = new LevelResourcesView();

        protected override void DisposeInner()
        {
            Clashes.Dispose();
            Hand.Dispose();
            Customers.Dispose();
            Resources.Dispose();
        }
    }
}
