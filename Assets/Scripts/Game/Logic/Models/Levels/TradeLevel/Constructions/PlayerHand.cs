using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class PlayerHand : Disposable
    {
        public event Action<ConstructionCard, ConstructionSource> OnAdded = delegate { };
        public event Action<ConstructionCard> OnRemoved = delegate { };

        public IReadOnlyCollection<ConstructionCard> Cards => _cards.AsReadOnly();
        private List<ConstructionCard> _cards { get; set; } = new List<ConstructionCard>();

        private MainLevelVariation _definition;

        public PlayerHand(MainLevelVariation definition, IReadOnlyCollection<ConstructionDefinition> hand)
        {
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
            if (hand == null) throw new ArgumentNullException(nameof(hand));
            foreach (var item in hand)
            {
                Add(item, ConstructionSource.StartingHand);
            }
        }

        protected override void DisposeInner()
        {
            foreach (var card in _cards)
                card.Dispose();
        }

        public void Remove(ConstructionCard card)
        {
            _cards.Remove(card);
            card.Dispose();
            OnRemoved(card);
        }

        public bool Contain(ConstructionCard card)
        {
            return _cards.Contains(card);
        }

        public void Add(ConstructionDefinition definition, ConstructionSource source)
        {
            var card = _cards.FirstOrDefault(x => x.Definition == definition);
            if (card != null)
            {
                card.Add(1, source);
            }
            else
            {
                card = new ConstructionCard(this, definition);
                _cards.Add(card);
                OnAdded(card, source);
            }

        }

        public enum ConstructionSource
        {
            StartingHand,
            LevelUp,
            NewWave
        }
    }
}
