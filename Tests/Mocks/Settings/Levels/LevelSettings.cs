using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Buildings.Features;
using Game.Tests.Mocks.Settings.Rewards;
using Game.Tests.Mocks.Views.Levels;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Mocks.Settings.Levels
{
    public class LevelSettings : ILevelSettings
    {
        public Point Size => new Point(4, 4);


        public int HandSize { get; set; } = 10;

        public IConstructionSettings[] StartingHand { get; } = new IConstructionSettings[] {
            new ConstructionSettings()
            {
                FeaturesList = new List<IConstructionFeatureSettings>()
                {
                    new OrderingPlaceConstructionFeatureSettings()
                }
            }
        };

        public Rect UnitsSpawnRect { get; set; } = new Rect(-5, -5, 10, 10);

        public float ClashTime { get; set; } = 20;

        public Dictionary<ICustomerSettings, int> Deck { get; } = new Dictionary<ICustomerSettings, int>()
        {
            { new CustomerSettings(), 1 }
        };

        public IReward ClashReward { get; set; } = new Reward();

        public int MinQueue { get; set; } = 0;

        public int MaxQueue { get; set; } = 0;
    }
}
