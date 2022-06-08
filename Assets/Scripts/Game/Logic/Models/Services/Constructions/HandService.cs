using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class HandService : IService
    {
        private readonly IRepository<ConstructionCard> _repository;
        private readonly IRepository<ConstructionScheme> _schemes;

        public HandService(IRepository<ConstructionCard> repository, IRepository<ConstructionScheme> schemes)
        {
            _repository = repository;
            _schemes = schemes;
        }

        public void Remove(ConstructionCard card)
        {
            card.Remove(new CardAmount(1));
            _repository.Save(card);
            if (card.Amount.Value == 0)
                _repository.Remove(card);
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
                _repository.Save(card);
                return card;
            }
            else
            {
                var newCard = new ConstructionCard(scheme, amount);
                _repository.Add(newCard);
                return newCard;
            }
        }

        public IReadOnlyCollection<ConstructionCard> GetCards()
        {
            return _repository.Get();
        }

        public bool Has(ConstructionCard card)
        {
            return _repository.Has(card);
        }
    }
}
