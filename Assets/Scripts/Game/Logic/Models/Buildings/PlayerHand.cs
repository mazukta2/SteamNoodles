using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Models.Buildings
{
    public class PlayerHand : Disposable
    {
        public event Action<ConstructionCard> OnAdded = delegate { };
        public event Action<ConstructionCard> OnRemoved = delegate { };

        public ConstructionCard[] Cards => _cards.ToArray();
        private List<ConstructionCard> _cards { get; set; } = new List<ConstructionCard>();

        public PlayerHand(IConstructionSettings[] startingHand)
        {
            foreach (var item in startingHand)
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
            OnRemoved(scheme);
        }

        public bool Contain(ConstructionCard scheme)
        {
            return _cards.Contains(scheme);
        }

        public void Add(IConstructionSettings proto)
        {
            Add(new ConstructionCard(proto));
        }

        private void Add(ConstructionCard buildingScheme)
        {
            _cards.Add(buildingScheme);
            OnAdded(buildingScheme);
        }

    }
}
