using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Constructions
{
    public class ConstructionCardPresentation : Disposable, IAggregation
    {
        public string HandImagePath { get; set; }
        public Uid Id { get; set; }
        public CardAmount Amount { get; set; }
        public LocalizationTag Name { get; set; }
        public BuildingPoints Points { get; set; }
        public AdjacencyBonuses AdjacencyPoints { get; set; }
        public IntRect Size { get; set; }

        public static ConstructionCardPresentation Default()
        {
            var card = new ConstructionCardPresentation();

            return card;
        }
    }
}