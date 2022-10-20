using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public interface IUnits
    {
        event Action<Unit> OnUnitSpawn;
        IReadOnlyCollection<Unit> Units { get; }
        float GetUnitSize();
        Unit SpawnUnit(GameVector3 pos);
        Unit SpawnUnit(GameVector3 pos, GameVector3 target)
        {
            var unit = SpawnUnit(pos);
            unit.SetTarget(target);
            return unit;
        }
        void DestroyUnit(Unit unit);
    }
}
