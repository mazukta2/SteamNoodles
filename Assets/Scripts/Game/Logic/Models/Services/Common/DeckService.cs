using Game.Assets.Scripts.Game.Logic.Models.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Common
{
    public class DeckService<T>
    {
        private readonly IGameRandom _random;
        private readonly ISingletonRepository<Deck<T>> _repository;

        public DeckService()
        {
            _random = new SessionRandom();
            _repository = new SingletonRepository<Deck<T>>();
            CreateDeck();
        }

        public DeckService(IGameRandom random)
        {
            _random = random;
            _repository = new SingletonRepository<Deck<T>>();
            CreateDeck();
        }

        public DeckService(ISingletonRepository<Deck<T>> repository, IGameRandom random)
        {
            _random = random;
            _repository = repository;
            CreateDeck();
        }

        public void Add(T element, int value = 1)
        {
            _repository.Get().Add(element, value);
        }

        public void Remove(T element, int value = 1)
        {
            _repository.Get().Remove(element, value);
        }

        public IReadOnlyDictionary<T, int> GetItems()
        {
            return _repository.Get().GetItems();
        }

        public ReadOnlyCollection<T> GetItemsList()
        {
            return _repository.Get().GetItemsList();
        }

        public T Take()
        {
            return _repository.Get().Take(_random);
        }

        public bool IsEmpty()
        {
            return _repository.Get().IsEmpty();
        }

        private void CreateDeck()
        {
            if (!_repository.Has()) _repository.Add(new Deck<T>());
        }
    }
}
