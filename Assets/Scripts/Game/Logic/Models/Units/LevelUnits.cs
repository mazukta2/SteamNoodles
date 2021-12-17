using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Effects.Systems;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class LevelUnits : Disposable
    {
        public event Action<Unit> OnUnitSpawn = delegate { };
        public event Action<Unit> OnUnitDestroy = delegate { };
        public IEnumerable<Unit> Units => _spawnedUnits;


        private Placement _placement;
        private IUnitsSettings _prototype;
        private SessionRandom _random;
        private GameTime _time;
        private UnitServicing _unitServingMoney;
        private int UnitsCount = 20;
        private List<Unit> _spawnedUnits = new List<Unit>();
        private List<Unit> _crowd = new List<Unit>();
        private Deck<ICustomerSettings> _pool;
        private Rect Rect => _prototype.UnitsSpawnRect;

        public LevelUnits(IUnitsSettings unitsSettings, Placement placement, Time.GameTime time, SessionRandom random, IUnitsSettings prototype, UnitServicing unitServingMoney)
        {
            _placement = placement ?? throw new ArgumentNullException(nameof(placement));
            _prototype = prototype ?? throw new ArgumentNullException(nameof(prototype));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _unitServingMoney = unitServingMoney ?? throw new ArgumentNullException(nameof(unitServingMoney));

            _pool = new Deck<ICustomerSettings>(random);
            foreach (var item in unitsSettings.Deck)
                _pool.Add(item.Key, item.Value);

            for (int i = 0; i < UnitsCount; i++)
            {
                SpawnUnit();
            }

            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
        }

        public void AddCustumer(ICustomerSettings customer)
        {
            _pool.Add(customer);
        }

        public void RemoveCustomer(ICustomerSettings customer)
        {
            _pool.Remove(customer);
        }

        private Unit SpawnUnit(ICustomerSettings settings = null)
        {
            if (settings == null)
                settings = _pool.Take();

            var position = Rect.GetRandomFloatPoint(_random);
            var unit = new Unit(position, new FloatPoint(_random.GetRandom() ? Rect.X - 1 : Rect.X + Rect.Width + 1, position.Y), settings, _unitServingMoney);
            _spawnedUnits.Add(unit);
            _crowd.Add(unit);
            OnUnitSpawn(unit);
            return unit;
        }

        private void Time_OnTimeChanged(float delta)
        {
            foreach (var item in _spawnedUnits.ToArray())
            {
                item.MoveToTarget(delta);
            }

            foreach (var item in _crowd.ToArray())
            {
                if (!Rect.IsHorisontalyInside(item.Position))
                {
                    _spawnedUnits.Remove(item);
                    _crowd.Remove(item);
                    OnUnitDestroy(item);
                }
            }

            if (_spawnedUnits.Count < UnitsCount)
            {
                var position = Rect.GetRandomFloatPoint(_random);
                FloatPoint target;
                if (_random.GetRandom())
                {
                    position = new FloatPoint(Rect.X + 1, position.Y);
                    target = new FloatPoint(Rect.X + Rect.Width + 1, position.Y);
                }
                else
                {
                    position = new FloatPoint(Rect.X + Rect.Width - 1, position.Y);
                    target = new FloatPoint(Rect.X - 1, position.Y);
                }
                var unit = new Unit(position, target, _pool.Take(), _unitServingMoney);
                _spawnedUnits.Add(unit);
                _crowd.Add(unit);
                OnUnitSpawn(unit);
            }
        }

        public void ReturnToCrowd(Unit unit)
        {
            _crowd.Add(unit);

            var position = Rect.GetRandomFloatPoint(_random);
            FloatPoint target;
            if (_random.GetRandom())
            {
                position = new FloatPoint(Rect.X + 1, position.Y);
                target = new FloatPoint(Rect.X + Rect.Width + 1, position.Y);
            }
            else
            {
                position = new FloatPoint(Rect.X + Rect.Width - 1, position.Y);
                target = new FloatPoint(Rect.X - 1, position.Y);
            }
            unit.SetTarget(target);
        }

        public Unit GetUnit(ICustomerSettings unitSetting)
        {
            var unitsOfType = _crowd.Where(x => x.CanOrder() && x.Settings == unitSetting);
            if (unitsOfType.Any())
            {
                return unitsOfType.OrderBy(u => Math.Abs(u.Position.X)).First();
            }
            return SpawnUnit(unitSetting);
        }

        public void TakeFromCrowd(Unit unit)
        {
            _crowd.Remove(unit);
        }
    }
}
