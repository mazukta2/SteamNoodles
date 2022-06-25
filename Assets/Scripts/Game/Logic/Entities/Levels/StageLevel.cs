using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Entities.Levels
{
    public record StageLevel : Level
    {
        public IReadOnlyCollection<ConstructionScheme> StartingSchemes { get; } = new List<ConstructionScheme>();
        public IntPoint PlacementFieldSize { get; private set; }
        public IReadOnlyDictionary<ConstructionScheme, int> ConstructionsReward { get; private set; } = new Dictionary<ConstructionScheme, int>();
        public IReadOnlyDictionary<UnitType, int> CrowdUnits { get; private set; } = new Dictionary<UnitType, int>();
        public FloatRect UnitsRect { get; private set; }
        public int CrowdUnitsAmount { get; private set; }
        public float CellSize { get; private set; }
        public float PieceSpawningTime { get; private set; }
        public float PieceMovingTime { get; private set; }
        public float LevelUpPower { get; private set; }
        public float LevelUpOffset { get; private set; }

        public StageLevel(LevelDefinition definition, ConstructionsSettingsDefinition constructionsSettingsDefinition, IRepository<ConstructionScheme> schemes, IRepository<UnitType> units) : base(definition)
        {
            PlacementFieldSize = definition.PlacementField.Size;

            var startingHand = new List<ConstructionScheme>();
            foreach (var construction in definition.StartingHand)
                startingHand.Add(schemes.Get().First(x => x.IsConnectedToDefinition(construction)));
            StartingSchemes = startingHand;

            var constructionsReward = new Dictionary<ConstructionScheme, int>();
            foreach (var construction in definition.ConstructionsReward)
                constructionsReward.Add(schemes.Get().First(x => x.IsConnectedToDefinition(construction.Key)), construction.Value);
            ConstructionsReward = constructionsReward;

            var crowdUnits = new Dictionary<UnitType, int>();
            foreach (var unit in definition.BaseCrowdUnits)
                crowdUnits.Add(units.Get().First(x => x.IsConnectedToDefinition(unit.Key)), unit.Value);
            CrowdUnits = crowdUnits;

            UnitsRect = definition.UnitsRect;
            CrowdUnitsAmount = definition.CrowdUnitsAmount;
            CellSize = constructionsSettingsDefinition.CellSize;
            PieceSpawningTime = constructionsSettingsDefinition.PieceSpawningTime;
            PieceMovingTime = constructionsSettingsDefinition.PieceMovingTime;
            LevelUpPower = constructionsSettingsDefinition.LevelUpPower;
            LevelUpOffset = constructionsSettingsDefinition.LevelUpOffset;
        }

        public StageLevel(IEnumerable<ConstructionScheme> startingSchemes)
        {
            StartingSchemes = startingSchemes.AsReadOnly();
        }

        public StageLevel()
        {
            CellSize = 1;
            PlacementFieldSize = new IntPoint(11, 11);
        }
    }
}
