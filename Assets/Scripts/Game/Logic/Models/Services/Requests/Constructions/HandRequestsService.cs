using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests
{
    public class HandRequestsService : Disposable, IService
    {
        private IPresenterRepository<ConstructionCard> _cards;
        private HandModel _hand;

        //BuildingModeService buildingService
        public HandRequestsService(IPresenterRepository<ConstructionCard> cards)
        {
            _cards = cards ?? throw new System.ArgumentNullException(nameof(cards));

            _hand = new HandModel();

            foreach (var card in _cards.Get())
                Add(card.Get());

            _cards.OnAdded += _cards_OnAdded;
            _cards.OnRemoved += _cards_OnRemoved;
        }

        protected override void DisposeInner()
        {
            _cards.OnAdded -= _cards_OnAdded;
            _cards.OnRemoved -= _cards_OnRemoved;
            _hand.Dispose();
        }

        public HandModel Get()
        {
            return _hand;
        }

        private void Add(ConstructionCard card)
        {
            var model = new ConstructionHandModel(card.Id);
            _hand.Add(model);
        }

        private void Remove(ConstructionCard card)
        {
            _hand.Remove(card.Id);
        }

        private void _cards_OnRemoved(EntityLink<ConstructionCard> arg1, ConstructionCard card)
        {
            Remove(card);
        }

        private void _cards_OnAdded(EntityLink<ConstructionCard> arg1, ConstructionCard card)
        {
            Add(card);
        }

    }
}
