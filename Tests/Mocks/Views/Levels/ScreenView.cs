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
        public DisposableViewKeeper<IClashesView> Clashes { get; } = new DisposableViewKeeper<IClashesView>(CreateClashes);
        public DisposableViewKeeper<IHandView> Hand { get; } = new DisposableViewKeeper<IHandView>(CreateHand);
        public DisposableViewKeeper<ICurrentOrderView> Customers { get; } = new DisposableViewKeeper<ICurrentOrderView>(CreateOrder);
        public DisposableViewKeeper<ILevelResourcesView> Resources { get; } = new DisposableViewKeeper<ILevelResourcesView>(CreateResources);

        public static IHandView CreateHand()
        {
            return new BasicHandView();
        }

        public static ICurrentOrderView CreateOrder()
        {
            return new BasicCurrentOrderView();
        }

        public static IClashesView CreateClashes()
        {
            return new ClashesView();
        }

        public static ILevelResourcesView CreateResources()
        {
            return new LevelResourcesView();
        }

        protected override void DisposeInner()
        {
            Clashes.Dispose();
            Hand.Dispose();
            Customers.Dispose();
            Resources.Dispose();
        }
    }
}
