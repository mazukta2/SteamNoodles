﻿using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.External
{
    public interface IDefinitions
    {
        T Get<T>();
        T Get<T>(string id);
        IReadOnlyCollection<T> GetList<T>();

        static IDefinitions Default { get; set; }
    }
}
