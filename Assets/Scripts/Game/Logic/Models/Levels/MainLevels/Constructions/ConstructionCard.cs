using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class ConstructionCard : Disposable
    {
        public event Action<int, PlayerHand.ConstructionSource> OnAdded = delegate { };
        public event Action<int> OnRemoved = delegate { };
        public ConstructionDefinition Definition { get; }
        public int Amount { get => _amount; }

        private PlayerHand _hand;
        private int _amount = 1;

        public ConstructionCard(PlayerHand hand, ConstructionDefinition definition)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            _hand = hand ?? throw new ArgumentNullException(nameof(hand));
        }

        public void Add(int value, PlayerHand.ConstructionSource source)
        {
            if (value < 0)
                throw new Exception("Cant add value");

            _amount += value;
            OnAdded(value, source);
        }

        public void Remove(int value)
        {
            if (value < 0)
                throw new Exception("Cant add value");

            _amount -= value;
            OnRemoved(value);
            if (_amount <= 0)
                _hand.Remove(this);
        }
    }
}
