using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicConstructionView : TestView, IConstructionView
    {
        public FloatPoint Position { get; private set; }
        public IVisual Visual { get; private set; }

        public void SetPosition(FloatPoint pos)
        {
            Position = pos;
        }

        public FloatPoint GetPosition()
        {
            return Position;
        }

        public IVisual GetImage()
        {
            return Visual;
        }

        public void SetImage(IVisual image)
        {
            Visual = image;
        }

        protected override void DisposeInner()
        {
        }
    }
}
