using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Definitions;
using Game.Assets.Scripts.Game.Logic.Services.Levels;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class StageLevelSpecifics : LevelSpecifics
    {
        public override Level CreateEntity(LevelDefinition definition, ServiceManager services)
        {
            var schemes = services.Get<IDatabase<ConstructionScheme>>();
            var units = services.Get<IDatabase<UnitType>>();
            var definitions = services.Get<DefinitionsService>();
            return new StageLevel(definition, definitions.Get<ConstructionsSettingsDefinition>(), schemes, units);
        }

        public override void StartServices(Level level)
        {
            if (level is not StageLevel stageLevel)
                throw new Exception($"Wrong type of level {level}");

            IModelServices.Default.Add(new StageLevelService(stageLevel));
            IModelServices.Default.Add(new StageLevelPresenterService(stageLevel));
        }

        public override void StartLevel(Level level)
        {
        }

    }
}
