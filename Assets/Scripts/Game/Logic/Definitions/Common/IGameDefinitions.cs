using Game.Assets.Scripts.Game.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Definitions
{
    public interface IGameDefinitions
    {
        static IGameDefinitions Default { get; set; }
        T Get<T>(string id);
        T Get<T>();
        IReadOnlyCollection<T> GetList<T>();
    }
}
