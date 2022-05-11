using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class ConstructionCard : Disposable
    {
        public event Action<int> OnAdded = delegate { };
        public event Action<int> OnRemoved = delegate { };
        public ConstructionDefinition Definition { get; }
        public int Amount { get => _amount; set => ChangeAmount(value); }

        private PlayerHand _hand;
        private int _amount = 1;

        public ConstructionCard(PlayerHand hand, ConstructionDefinition definition)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _hand = hand ?? throw new ArgumentNullException(nameof(hand));
        }

        private void ChangeAmount(int newValue)
        {
            var diff = newValue - _amount;
            _amount = newValue;
            if (diff > 0)
            {
                OnAdded(diff);
            }
            else if (diff < 0)
            {
                OnRemoved(-diff);
                if (_amount <= 0)
                    _hand.Remove(this);
            }
        }

    }
}
