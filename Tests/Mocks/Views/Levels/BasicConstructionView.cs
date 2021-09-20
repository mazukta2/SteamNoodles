using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
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
    }
}
