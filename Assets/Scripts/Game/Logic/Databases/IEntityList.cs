using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Databases
{
    public interface IEntityList<out T> where T : class
    {
        IReadOnlyCollection<T> Get();
        T Get(Uid uid);
    }
}
