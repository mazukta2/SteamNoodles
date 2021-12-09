using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Core
{
    public static class CollectionsHelper
    {
        public static IReadOnlyCollection<T> AsReadOnly<T>(this List<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }

        public static IReadOnlyCollection<T> AsReadOnly<T>(this T[] list)
        {
            return new ReadOnlyCollection<T>(list);
        }
    }
}
