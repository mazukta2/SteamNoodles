using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class HandService : IService
    {
        private readonly IDatabase<ConstructionCardEntity> _database;

        public HandService(IDatabase<ConstructionCardEntity> database)
        {
            _database = database;
        }

        public void Remove(ConstructionCardEntity cardEntity)
        {
            cardEntity.Remove(new CardAmount(1));
            if (cardEntity.Amount.Value == 0)
                _database.Remove(cardEntity.Id);
        }

        public ConstructionCardEntity Add(ConstructionSchemeEntity schemeEntity)
        {
            return Add(schemeEntity, new CardAmount(1));
        }

        public ConstructionCardEntity Add(ConstructionSchemeEntity schemeEntity, CardAmount amount)
        {
            if (schemeEntity is null) throw new ArgumentNullException(nameof(schemeEntity));

            var card = GetCards().FirstOrDefault(x => x.SchemeEntity == schemeEntity);
            if (card != null)
            {
                card.Add(amount);
                return card;
            }
            else
            {
                var newCard = new ConstructionCardEntity(schemeEntity, amount);
                _database.Add(newCard);
                return newCard;
            }
        }

        public IReadOnlyCollection<ConstructionCardEntity> GetCards()
        {
            return _database.Get();
        }

        public bool Has(ConstructionCardEntity cardEntity)
        {
            return _database.Has(cardEntity);
        }
    }
}
