using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using Game.Tests.Mocks.Views.Clashes;
using Game.Tests.Mocks.Views.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;
using Tests.Tests.Mocks.Views.Levels;

namespace Game.Tests.Mocks.Views.Levels
{
    public class ScreenView : TestView, IScreenView
    {
        public DisposableViewKeeper<IClashesView> Clashes { get; } = new DisposableViewKeeper<IClashesView>(CreateClashes);
        public DisposableViewKeeper<IHandView> Hand { get; } = new DisposableViewKeeper<IHandView>(CreateHand);

        public static IHandView CreateHand()
        {
            return new BasicHandView();
        }

        public ICurrentOrderView CreateCurrentOrder()
        {
            return new BasicCurrentOrderView();
        }

        public static IClashesView CreateClashes()
        {
            return new ClashesView();
        }
        protected override void DisposeInner()
        {
            Clashes.Dispose();
            Hand.Dispose();
        }
    }
}
