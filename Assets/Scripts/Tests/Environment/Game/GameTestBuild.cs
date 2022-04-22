﻿using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Managers.Game
{
    public class GameTestBuild : IDisposable
    {
        public GameEngineInTests Engine { get; private set; }
        public Core Core { get; private set; }

        public GameModel GameModel => Core?.Game;
        public LevelView CurrentLevel => (LevelView)Engine.Levels.GetCurrentLevel();

        private List<IDisposable> _toDispose = new List<IDisposable>();

        public GameTestBuild(Core core, GameEngineInTests gameEngine)
        {
            Core = core;
            Engine = gameEngine;

            new DefaultLocalizationService().Set(new LocalizationManagerMock());
        }

        public void Dispose()
        {
            new DefaultLocalizationService().Clear();

            foreach (var item in _toDispose)
                item.Dispose();
            _toDispose.Clear();

            Core.Dispose();
            Engine.Assets.Dispose();
            Engine.Levels.Dispose();

            Core = null;
            Engine = null;
        }

        public void LoadLevel(LevelDefinitionMock loadLevel)
        {
            var session = GameModel.CreateSession();
            var levelLoading = session.LoadLevel(loadLevel);
            GameLevel newLevel = null;

            levelLoading.OnLoaded += HandleOnLoaded;
            Engine.Levels.FinishLoading();
            levelLoading.OnLoaded -= HandleOnLoaded;

            _toDispose.Add(newLevel);
            _toDispose.Add(session);

            void HandleOnLoaded(GameLevel level)
            {
                newLevel = level;
            }
        }

    }
}
