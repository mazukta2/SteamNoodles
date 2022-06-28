using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Cards
{
    public class CardConstruction : Disposable, IAggregation
    {
        public string HandImagePath { get; set; }
        public Uid Id { get; set; }
        public CardAmount Amount { get; set; }
        public LocalizationTag Name { get; set; }
        public BuildingPoints Points { get; set; }
        public AdjacencyBonuses AdjacencyPoints { get; set; }
        public IntRect Size { get; set; }

        public static CardConstruction Default()
        {
            var card = new CardConstruction();

            return card;
        }
    }
}