using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Assets.Scripts.Models.Buildings
{
    public class PlayerHand : Disposable
    {
        public event Action<ConstructionCard> OnAdded = delegate { };
        public event Action<ConstructionCard> OnRemoved = delegate { };

        public ReadOnlyCollection<ConstructionCard> Cards => _cards.AsReadOnly();
        private List<ConstructionCard> _cards { get; set; } = new List<ConstructionCard>();

        private ILevelSettings _settings;

        public PlayerHand(ILevelSettings settings)
        {
            _settings = settings;
            foreach (var item in settings.StartingHand)
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

        public void Add(IConstructionSettings proto)
        {
            Add(new ConstructionCard(proto));
            if (_cards.Count > _settings.HandSize)
                Remove(_cards.First());
        }

        private void Add(ConstructionCard buildingScheme)
        {
            _cards.Add(buildingScheme);
            OnAdded(buildingScheme);
        }

    }
}
