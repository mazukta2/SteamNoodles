using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System.Numerics;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicConstructionView : TestView, IConstructionView
    {
        public Vector2 Position { get; private set; }
        public IVisual Visual { get; private set; }

        public void SetPosition(Vector2 pos)
        {
            Position = pos;
        }

        public Vector2 GetPosition()
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
