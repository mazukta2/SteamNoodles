using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Tests.Mocks.Views.Levels;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelSettings : ILevelSettings
    {
        public Point Size => new Point(4, 4);

        public IConstructionSettings[] StartingHand => new IConstructionSettings[] {
            new ConstructionSettings()
        };

        public Rect UnitsSpawnRect { get; set; } = new Rect(-5, -5, 10, 10);

        public float ClashTime { get; set; } = 20;
    }
}
