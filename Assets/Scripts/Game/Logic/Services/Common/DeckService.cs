using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.Assets.Scripts.Game.Logic.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Session;

namespace Game.Assets.Scripts.Game.Logic.Services.Common
{
    public class DeckService<T>
    {
        private readonly IGameRandom _random;
        private readonly ISingletonDatabase<Deck<T>> _database;

        public DeckService()
        {
            _random = new SessionRandom();
            _database = new SingletonDatabase<Deck<T>>();
            CreateDeck();
        }

        public DeckService(IGameRandom random)
        {
            _random = random;
            _database = new SingletonDatabase<Deck<T>>();
            CreateDeck();
        }

        public DeckService(ISingletonDatabase<Deck<T>> database, IGameRandom random)
        {
            _random = random;
            _database = database;
            CreateDeck();
        }

        public void Add(T element, int value = 1)
        {
            _database.Get().Add(element, value);
        }

        public void Remove(T element, int value = 1)
        {
            _database.Get().Remove(element, value);
        }

        public IReadOnlyDictionary<T, int> GetItems()
        {
            return _database.Get().GetItems();
        }

        public ReadOnlyCollection<T> GetItemsList()
        {
            return _database.Get().GetItemsList();
        }

        public T Take()
        {
            return _database.Get().Take(_random);
        }

        public bool IsEmpty()
        {
            return _database.Get().IsEmpty();
        }

        private void CreateDeck()
        {
            if (!_database.Has()) _database.Add(new Deck<T>());
        }
    }
}
