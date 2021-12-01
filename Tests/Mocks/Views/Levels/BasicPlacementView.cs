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

        public void SetClick(Action<Vector2> onClick)
        {
            _onClick = onClick;
        }

        public void Click(Vector2 vector2)
        {
            _onClick(vector2);
        }

        public IGhostConstructionView CreateGhost()
        {
            return new BasicGhostView();
        }

        public IConstructionView CreateConstrcution()
        {
            return new BasicConstructionView();
        }

        public ICellView CreateCell()
        {
            return new BasicCellView();
        }
        protected override void DisposeInner()
        {
        }
    }
}
