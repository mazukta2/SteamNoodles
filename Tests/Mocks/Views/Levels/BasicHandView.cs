using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Levels
{
    public class BasicHandView : TestView, IHandView
    {
        public DisposableViewListKeeper<IHandConstructionView> Cards { get; } = new DisposableViewListKeeper<IHandConstructionView>(SpawnCard);

        private static IHandConstructionView SpawnCard()
        {
            return new BasicHandConstructionView();
        }

        protected override void DisposeInner()
        {
            Cards.Dispose();
        }
    }
}
