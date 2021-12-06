using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Calculations
{
    public class Deck<T>
    {
        public Dictionary<T, int> _pool = new Dictionary<T, int>();
        public Dictionary<T, int> _taken = new Dictionary<T, int>();
        private SessionRandom _random;

        public Deck(SessionRandom random)
        {
            _random = random;
        }

        public void Add(T element, int value)
        {
            if (_pool.ContainsKey(element))
                _pool[element] += value;
            else 
                _pool.Add(element, value);
        }

        public ReadOnlyDictionary<T, int> GetItems()
        {
            return new ReadOnlyDictionary<T, int>(_pool);
        }

        public T Take()
        {
            if (IsEmpty())
                throw new Exception("Cant take from empty deck");

            var existing = GetExisting();
            var totalCount = existing.Values.Sum();
            if (totalCount == 0)
            {
                foreach (var item in _taken)
                    _taken[item.Key] = 0;
                existing = GetExisting();
                totalCount = existing.Values.Sum();
            }

            var index = _random.GetRandom(0, totalCount);

            var list = existing.ToList().OrderBy(x => x.Key);
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
                var taken = _taken.GetValueOrDefault(element.Key);
                result.Add(element.Key, element.Value - taken);
            }
            return result;
        }
    }
}
