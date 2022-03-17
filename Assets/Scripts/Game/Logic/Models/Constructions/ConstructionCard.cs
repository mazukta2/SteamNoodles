using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class ConstructionCard : Disposable
    {
        public ConstructionDefinition Definition { get; }

        private PlayerHand _hand;

        public ConstructionCard(PlayerHand hand, ConstructionDefinition definition)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _hand = hand ?? throw new ArgumentNullException(nameof(hand));
        }

        public void RemoveFromHand()
        {
            _hand.Remove(this);
        }
    }
}
