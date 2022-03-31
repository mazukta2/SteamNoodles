using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Common.Calculations
{
    public class Deck<T>
    {
        private readonly Dictionary<T, int> _pool = new Dictionary<T, int>();
        private readonly Dictionary<T, int> _taken = new Dictionary<T, int>();
        private readonly SessionRandom _random;

        public Deck(SessionRandom random)
        {
            _random = random;
        }

        public void Add(T element, int value = 1)
        {
            if (_pool.ContainsKey(element))
                _pool[element] += value;
            else 
                _pool.Add(element, value);
        }

        public void Remove(T element, int value = 1)
        {
            if (!_pool.ContainsKey(element))
                throw new Exception("No such elements");

            if (_pool[element] < value)
                throw new Exception("No enough elements");

            _pool[element] -= value;
        }

        public ReadOnlyDictionary<T, int> GetItems()
        {
            return new ReadOnlyDictionary<T, int>(_pool);
        }

        public ReadOnlyCollection<T> GetItemsList()
        {
            return _pool.Keys.ToList().AsReadOnly();
        }

        public T Take()
        {
            if (IsEmpty())
                throw new Exception("Cant take from empty deck");

            var existing = GetExisting();
            var totalCount = existing.Values.Sum();
            if (totalCount == 0)
            {
                foreach (var item in _taken.ToList())
                    _taken[item.Key] = 0;
                existing = GetExisting();
                totalCount = existing.Values.Sum();
            }

            var index = _random.GetRandom(0, totalCount);

            var list = existing.ToList();
            var minIndex = 0;
            foreach (var item in list)
            {
                if (minIndex <= index && index < minIndex + item.Value)
                {
                    if (_taken.ContainsKey(item.Key))
                        _taken[item.Key] += 1;
                    else
                        _taken.Add(item.Key, 1);
                    return item.Key;
                }

                minIndex += item.Value;
            }

            throw new Exception("Cant find elements");
        }

        public bool IsEmpty()
        {
            return _pool.Values.Sum() == 0;
        }

        private Dictionary<T, int> GetExisting()
        {
            var result = new Dictionary<T, int>();
            foreach (var element in _pool)
            {
                var taken = 0;
                if (_taken.ContainsKey(element.Key))
                    taken = _taken[element.Key];
                result.Add(element.Key, element.Value - taken);
            }
            return result;
        }
    }
}
