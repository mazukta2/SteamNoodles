using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class PlayerHand : Disposable
    {
        public event Action<ConstructionCard> OnAdded = delegate { };
        public event Action<ConstructionCard> OnRemoved = delegate { };

        public IReadOnlyCollection<ConstructionCard> Cards => _cards.AsReadOnly();
        private List<ConstructionCard> _cards { get; set; } = new List<ConstructionCard>();

        private LevelDefinition _definition;

        public PlayerHand(LevelDefinition definition, IReadOnlyCollection<ConstructionDefinition> hand)
        {
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
            if (hand == null) throw new ArgumentNullException(nameof(hand));
            foreach (var item in hand)
            {
                Add(new ConstructionCard(item));
            }
        }

        protected override void DisposeInner()
        {
            foreach (var card in _cards)
                card.Dispose();
            _cards = null;
        }

        public void Remove(ConstructionCard scheme)
        {
            _cards.Remove(scheme);
            scheme.Dispose();
            OnRemoved(scheme);
        }

        public bool Contain(ConstructionCard scheme)
        {
            return _cards.Contains(scheme);
        }

        public void Add(ConstructionDefinition proto)
        {
            Add(new ConstructionCard(proto));
            if (_cards.Count > _definition.HandSize)
                Remove(_cards.First());
        }

        private void Add(ConstructionCard buildingScheme)
        {
            _cards.Add(buildingScheme);
            OnAdded(buildingScheme);
        }

    }
}
