using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public interface IUnits
    {
        float GetUnitSize();
        Unit SpawnUnit(FloatPoint3D pos);
        Unit SpawnUnit(FloatPoint3D pos, FloatPoint3D target)
        {
            var unit = SpawnUnit(pos);
            unit.SetTarget(target);
            return unit;
        }
        void DestroyUnit(Unit unit);
    }
}
