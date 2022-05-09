using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class LevelUnits : Disposable
    {
        public event Action<Unit> OnUnitSpawn = delegate { };
        public event Action<Unit> OnUnitDestroy = delegate { };
        public IReadOnlyCollection<Unit> Units => _spawnedUnits;

        private List<Unit> _spawnedUnits = new List<Unit>();
        private IGameTime _time;

        public LevelUnits(IGameTime time)
        {
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
            foreach (var unit in _spawnedUnits)
                unit.Dispose();
        }

        public Unit SpawnUnit(Unit unit)
        {
            _spawnedUnits.Add(unit);
            OnUnitSpawn(unit);
            return unit;
        }

        public void RemoveUnit(Unit item)
        {
            _spawnedUnits.Remove(item);
            item.Dispose();
            OnUnitDestroy(item);
        }

        private void Time_OnTimeChanged(float oldTime, float newTime)
        {
            foreach (var item in _spawnedUnits.ToArray())
            {
                item.MoveToTarget(newTime - oldTime);
            }
        }


        //public void ReturnToCrowd(Unit unit)
        //{
        //    _crowd.Add(unit);

        //    var position = Rect.GetRandomFloatPoint(_random);
        //    FloatPoint target;
        //    if (_random.GetRandom())
        //    {
        //        position = new FloatPoint(Rect.X + 1, position.Y);
        //        target = new FloatPoint(Rect.X + Rect.Width + 1, position.Y);
        //    }
        //    else
        //    {
        //        position = new FloatPoint(Rect.X + Rect.Width - 1, position.Y);
        //        target = new FloatPoint(Rect.X - 1, position.Y);
        //    }
        //    unit.SetTarget(target);
        //}

        //public Unit GetUnit(ICustomerSettings unitSetting)
        //{
        //    var unitsOfType = _crowd.Where(x => x.CanOrder() && x.Settings == unitSetting);
        //    if (unitsOfType.Any())
        //    {
        //        return unitsOfType.OrderBy(u => Math.Abs(u.Position.X)).First();
        //    }
        //    return SpawnUnit(unitSetting);
        //}

        //public void TakeFromCrowd(Unit unit)
        //{
        //    _crowd.Remove(unit);
        //}


    }
}
