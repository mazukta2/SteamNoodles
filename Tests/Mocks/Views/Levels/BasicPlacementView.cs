using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicPlacementView : TestView, IPlacementView
    {
        private Action<Vector2> _onClick;

        public DisposableViewKeeper<IGhostConstructionView> Ghost { get; } = new DisposableViewKeeper<IGhostConstructionView>(CreateGhost);
        public DisposableViewListKeeper<ICellView> Cells { get; } = new DisposableViewListKeeper<ICellView>(CreateCell);
        public DisposableViewListKeeper<IConstructionView> Constructions { get; } = new DisposableViewListKeeper<IConstructionView>(CreateConstrcution);

        public void SetClick(Action<Vector2> onClick)
        {
            _onClick = onClick;
        }

        public void Click(Vector2 vector2)
        {
            _onClick(vector2);
        }

        private static IGhostConstructionView CreateGhost()
        {
            return new BasicGhostView();
        }

        private static IConstructionView CreateConstrcution()
        {
            return new BasicConstructionView();
        }

        private static ICellView CreateCell()
        {
            return new BasicCellView();
        }

        protected override void DisposeInner()
        {
            Ghost.Dispose();
            Cells.Dispose();
            Constructions.Dispose();
        }
    }
}
