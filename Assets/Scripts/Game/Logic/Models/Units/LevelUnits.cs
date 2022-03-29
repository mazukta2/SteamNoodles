using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class LevelUnits : Disposable
    {
        public event Action<Unit> OnUnitSpawn = delegate { };
        public event Action<Unit> OnUnitDestroy = delegate { };
        public IReadOnlyCollection<Unit> Units => _spawnedUnits;


        private UnitsSettingsDefinition _unitsSettings;
        private SessionRandom _random;

        private LevelDefinition _levelDefinition;
        private Deck<CustomerDefinition> _pool;
        private List<Unit> _spawnedUnits = new List<Unit>();
        private List<Unit> _crowd = new List<Unit>();
        private GameTime _time;
        private Resources _resources;
        private PlayerHand _hand;

        private FloatRect UnitsField => _levelDefinition.UnitsRect;

        public LevelUnits(UnitsSettingsDefinition unitsSettings, LevelDefinition levelDefinition, GameTime time,
            SessionRandom random, Resources resources, PlayerHand hand)
        {
            _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _unitsSettings= unitsSettings ?? throw new ArgumentNullException(nameof(unitsSettings));
            _random = random ?? throw new ArgumentNullException(nameof(random));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _hand = hand ?? throw new ArgumentNullException(nameof(hand));

            _pool = new Deck<CustomerDefinition>(random);
            foreach (var item in levelDefinition.BaseCrowdUnits)
                _pool.Add(item.Key, item.Value);

            for (int i = 0; i < levelDefinition.CrowdUnitsAmount; i++)
            {
                SpawnUnit();
            }

            _time.OnTimeChanged += Time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
            foreach (var unit in _spawnedUnits)
                unit.Dispose();
        }

        private Unit SpawnUnit(CustomerDefinition definition = null)
        {
            if (definition == null)
                definition = _pool.Take();

            var position = GetRandomPoint(UnitsField, _random);
            var unit = new Unit(position, new FloatPoint(_random.GetRandom() ? UnitsField.X - 1 : UnitsField.X + UnitsField.Width + 1, position.Y), definition, _unitsSettings);
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
                if (!IsHorisontalyInside(UnitsField, item.Position))
                {
                    _spawnedUnits.Remove(item);
                    _crowd.Remove(item);
                    item.Dispose();
                    OnUnitDestroy(item);
                }
            }

            if (_crowd.Count < _levelDefinition.CrowdUnitsAmount)
            {
                var position = GetRandomPoint(UnitsField, _random);
                FloatPoint target;
                if (_random.GetRandom())
                {
                    position = new FloatPoint(UnitsField.X + 1, position.Y);
                    target = new FloatPoint(UnitsField.X + UnitsField.Width + 1, position.Y);
                }
                else
                {
                    position = new FloatPoint(UnitsField.X + UnitsField.Width - 1, position.Y);
                    target = new FloatPoint(UnitsField.X - 1, position.Y);
                }
                var unit = new Unit(position, target, _pool.Take(), _unitsSettings);
                _spawnedUnits.Add(unit);
                _crowd.Add(unit);
                OnUnitSpawn(unit);
            }
        }

        public bool IsHorisontalyInside(FloatRect rect, FloatPoint point)
        {
            return rect.xMin <= point.X && point.X <= rect.xMax;
        }

        private FloatPoint GetRandomPoint(FloatRect rect, SessionRandom random)
        {
            return new FloatPoint(random.GetRandom(rect.xMin, rect.xMax), random.GetRandom(rect.yMin, rect.yMax));
        }

        public void MoveQueue()
        {
            _resources.Points -= 1;

            var customer = _levelDefinition.BaseCrowdUnits.Keys.ToList()[_random.GetRandom(0, _levelDefinition.BaseCrowdUnits.Count - 1)];

            var deck = new Deck<ConstructionDefinition>(_random);
            foreach (var item in customer.ConstructionsReward)
                deck.Add(item.Key, item.Value);

            if (deck.IsEmpty())
                return;

            var constrcution = deck.Take();
            _hand.Add(constrcution);
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
