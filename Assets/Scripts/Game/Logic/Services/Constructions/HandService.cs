using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class HandService : IService
    {
        private readonly IDatabase<ConstructionCard> _database;

        public HandService(IDatabase<ConstructionCard> database)
        {
            _database = database;
        }

        public void Remove(ConstructionCard card)
        {
            card.Remove(new CardAmount(1));
            if (card.Amount.Value == 0)
                _database.Remove(card);
        }

        public ConstructionCard Add(ConstructionScheme scheme)
        {
            return Add(scheme, new CardAmount(1));
        }

        public ConstructionCard Add(ConstructionScheme scheme, CardAmount amount)
        {
            if (scheme is null) throw new ArgumentNullException(nameof(scheme));

            var card = GetCards().FirstOrDefault(x => x.Scheme == scheme);
            if (card != null)
            {
                card.Add(amount);
                return card;
            }
            else
            {
                var newCard = new ConstructionCard(scheme, amount);
                _database.Add(newCard);
                return newCard;
            }
        }

        public IReadOnlyCollection<ConstructionCard> GetCards()
        {
            return _database.Get();
        }

        public bool Has(ConstructionCard card)
        {
            return _database.Has(card);
        }
    }
}
