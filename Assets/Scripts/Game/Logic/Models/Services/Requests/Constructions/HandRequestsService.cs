using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests
{
    public class HandRequestsService : Disposable, IService
    {
        public event Action<IConstructionHandModel> OnAdded = delegate { };
        public event Action<IConstructionHandModel> OnRemoved = delegate { };

        private IPresenterRepository<ConstructionCard> _cards;
        private Model _hand;
        private List<IConstructionHandModel> _list = new List<IConstructionHandModel>();

        public HandRequestsService(IPresenterRepository<ConstructionCard> cards)
        {
            _cards = cards ?? throw new ArgumentNullException(nameof(cards));
            _hand = new Model(this);

            foreach (var card in _cards.Get())
                Add(card.Get());

            _cards.OnAdded += _cards_OnAdded;
            _cards.OnRemoved += _cards_OnRemoved;
        }

        protected override void DisposeInner()
        {
            foreach (var card in _list)
                card.Dispose();

            _cards.OnAdded -= _cards_OnAdded;
            _cards.OnRemoved -= _cards_OnRemoved;
            _hand.Dispose();
        }

        public Model Get()
        {
            return _hand;
        }

        private void Add(ConstructionCard card)
        {
            var model = new ConstructionHandModel(card.Id);
            _list.Add(model);
            OnAdded(model);
        }

        private void Remove(ConstructionCard card)
        {
            var model = Get(card.Id);
            model.Dispose();
            _list.Remove(model);
            OnRemoved(model);
        }

        private void _cards_OnRemoved(EntityLink<ConstructionCard> arg1, ConstructionCard card)
        {
            Remove(card);
        }

        private void _cards_OnAdded(EntityLink<ConstructionCard> arg1, ConstructionCard card)
        {
            Add(card);
        }

        private IConstructionHandModel Get(Uid id)
        {
            return _list.First(x => x.Id == id);
        }

        public class Model : Disposable, IHandModel
        {
            public event Action<IConstructionHandModel> OnAdded = delegate { };
            public event Action<IConstructionHandModel> OnRemoved = delegate { };

            private HandRequestsService _service;
            public Model(HandRequestsService service)
            {
                _service = service;
                _service.OnAdded += OnAddedHandler;
                _service.OnRemoved += OnRemovedHandler;
            }

            protected override void DisposeInner()
            {
                _service.OnAdded -= OnAddedHandler;
                _service.OnRemoved -= OnRemovedHandler;
            }

            private void OnAddedHandler(IConstructionHandModel obj) => OnAdded(obj);
            private void OnRemovedHandler(IConstructionHandModel obj) => OnRemoved(obj);
            public IReadOnlyCollection<IConstructionHandModel> GetCards() => _service._list.AsReadOnly();
        }
    }
}
