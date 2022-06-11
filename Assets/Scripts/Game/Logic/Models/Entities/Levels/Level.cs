using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Levels
{
    public record Level : Entity
    {
        public string SceneName { get; private set; }

        private Action<Level> _startServices;
        private Action<Level> _startLevel;

        public Level()
        {
            SceneName = "not_impotant";
            _startServices = (l) => { };
            _startLevel = (l) => { };
        }

        public Level(LevelDefinition levelDefinition)
        {
            SceneName = levelDefinition.SceneName;

            if (levelDefinition.Starter == null)
            {
                _startServices = (l) => { };
                _startLevel = (l) => { };
            }
            else
            {
                _startServices = levelDefinition.Starter.StartServices;
                _startLevel = levelDefinition.Starter.StartLevel;
            }
        }

        public void StartServices()
        {
            _startServices(this);
        }

        public void StartLevel()
        {
            _startLevel(this);
        }
    }
}
