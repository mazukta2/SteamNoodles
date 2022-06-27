using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.DataObjects.Constructions
{
    public struct ConstructionCardData : IData
    {
        public string HandImagePath { get; set; }
        public Uid Id { get; set; }
        public CardAmount Amount { get; set; }
        public LocalizationTag Name { get; set; }
        public BuildingPoints Points { get; set; }
        public AdjacencyBonuses AdjacencyPoints { get; set; }
        public IntRect Size { get; set; }

        public static ConstructionCardData Default()
        {
            var card = new ConstructionCardData();

            return card;
        }
    }
}