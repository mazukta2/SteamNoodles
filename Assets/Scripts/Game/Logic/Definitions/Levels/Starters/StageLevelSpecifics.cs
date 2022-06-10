﻿using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Definitions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class StageLevelSpecifics : LevelSpecifics
    {
        public override Level CreateEntity(LevelDefinition definition, ServiceManager services)
        {
            var schemes = services.Get<IRepository<ConstructionScheme>>();
            var units = services.Get<IRepository<UnitType>>();
            var definitions = services.Get<DefinitionsService>();
            return new StageLevel(definition, definitions.Get<ConstructionsSettingsDefinition>(), schemes, units);
        }

        public override void StartServices(Level level)
        {
            if (level is not StageLevel stageLevel)
                throw new Exception($"Wrong type of level {level}");

            IModelServices.Default.Add(new StageLevelService(stageLevel));
        }

        public override void StartLevel(Level level)
        {
            //ScreenManagerPresenter.Default.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

    }
}