﻿using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Common
{
    public interface IDefinitions
    {
        static IDefinitions Default { get; set; }
        T Get<T>(string id);
        T Get<T>();
        IReadOnlyCollection<T> GetList<T>();

    }
}
