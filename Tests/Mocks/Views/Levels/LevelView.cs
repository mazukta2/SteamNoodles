using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
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
    public class LevelView : TestView, ILevelView
    {
        public IScreenView Screen { get; private set; }

        private Action<float> _moveTime;

        public LevelView()
        {
            Screen = new ScreenView();
        }

        protected override void DisposeInner()
        {
            Screen.Dispose();
        }

        public IHandView CreateHand()
        {
            return new BasicHandView();
        }

        public IPlacementView CreatePlacement()
        {
            return new BasicPlacementView();
        }

        public ICurrentOrderView CreateCurrentOrder()
        {
            return new BasicCurrentOrderView();
        }

        public void SetTimeMover(Action<float> moveTime)
        {
            _moveTime = moveTime;
        }

        public IUnitsView CreateUnits()
        {
            return new UnitsView();
        }

        public IClashesView CreateClashes()
        {
            return new ClashesView();
        }
    }
}
