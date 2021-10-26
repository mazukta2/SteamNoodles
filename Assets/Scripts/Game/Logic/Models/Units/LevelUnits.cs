using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class LevelUnits
    {
        private Placement _placement;
        private IUnitsPrototype _prototype;
        private SessionRandom _random;
        private int UnitsCount = 40;
        private List<Unit> _spawnedUnits = new List<Unit>();
        private Rect Rect => _prototype.UnitsSpawnRect;

        public LevelUnits(Placement placement, SessionRandom random, IUnitsPrototype prototype)
        {
            _placement = placement;
            _prototype = prototype;
            _random = random;

            for (int i = 0; i < UnitsCount; i++)
            {
                SpawnUnit();
            }
        }

        public event Action<Unit> OnUnitSpawn = delegate { };
        public IEnumerable<Unit> Units => _spawnedUnits;

        private void SpawnUnit()
        {
            var position = Rect.GetRandomPoint(_random);
            var unit = new Unit(position);
            _spawnedUnits.Add(unit);
            OnUnitSpawn(unit);
        }
    }
}
