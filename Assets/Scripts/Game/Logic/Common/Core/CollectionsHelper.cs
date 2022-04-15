using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Core
{
    public static class CollectionsHelper
    {
        public static IReadOnlyCollection<T> AsReadOnly<T>(this List<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }

        public static IReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> list)
        {
            return new ReadOnlyCollection<T>(list.ToList());
        }

        public static IReadOnlyCollection<T> AsReadOnly<T>(this T[] list)
        {
            return new ReadOnlyCollection<T>(list);
        }
        public static IReadOnlyDictionary<T1, T2> AsReadOnly<T1, T2>(this IDictionary<T1, T2> list)
        {
            return new ReadOnlyDictionary<T1, T2>(list);
        }

        public static T GetRandom<T>(this T[] array, Random random)
        {
            return array[random.Next(array.Length)];
        }
    }
}
