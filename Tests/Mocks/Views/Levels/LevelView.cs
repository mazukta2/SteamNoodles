using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using Game.Tests.Mocks.Views.Clashes;
using Game.Tests.Mocks.Views.Units;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Tests.Mocks.Views.Common;
using Tests.Tests.Mocks.Views.Levels;

namespace Game.Tests.Mocks.Views.Levels
{
    public class LevelView : TestView, ILevelView
    {
        public IScreenView Screen { get; private set; }
        public IPlacementView Placement { get; private set; }
        public IUnitsView Units { get; private set; }

        public LevelView()
        {
            Screen = new ScreenView();
            Placement = new BasicPlacementView();
            Units = new UnitsView();
        }

        protected override void DisposeInner()
        {
            Screen.Dispose();
            Placement.Dispose();
            Units.Dispose();
        }
    }
}
