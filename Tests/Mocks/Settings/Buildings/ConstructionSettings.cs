using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Tests.Mocks.Views.Common;
using System.Collections.Generic;

namespace Game.Tests.Mocks.Settings.Buildings
{
    public class ConstructionSettings : IConstructionSettings
    {
        public Point Size { get; set; } = new Point(2, 1);

        public Requirements Requirements { get; set; } = new Requirements()
        {
            DownEdge = false
        };

        public ISprite HandIcon { get; } = new ItsUnitySpriteWrapper();

        public IVisual BuildingView { get; } = new ItsUnitySpriteWrapper();

        public IReadOnlyCollection<IConstructionFeatureSettings> Features => FeaturesList.AsReadOnly();
        public List<IConstructionFeatureSettings> FeaturesList = new List<IConstructionFeatureSettings>();

        public IReadOnlyDictionary<ConstructionTag, int> Tags => TagsList.AsReadOnly();
        public Dictionary<ConstructionTag, int> TagsList = new Dictionary<ConstructionTag, int>();
    }
}
