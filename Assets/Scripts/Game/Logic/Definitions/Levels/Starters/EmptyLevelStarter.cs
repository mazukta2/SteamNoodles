﻿using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Starters
{
    public class EmptyLevelStarter : LevelStarter
    {
        public override ILevel CreateModel(LevelDefinition definition)
        {
            return new GameLevel(definition, IGameRandom.Default, IGameTime.Default, IGameDefinitions.Default);
        }

        public override void Start()
        {
        }
    }
}