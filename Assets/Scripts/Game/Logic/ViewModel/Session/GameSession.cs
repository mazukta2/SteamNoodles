using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Common;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Session
{
    public class GameSessionViewModel
    {
        public AutoUpdatedProperty<LevelViewModel, LevelLoadedEvent> CurrentLevel { get; private set; }
        private GameSession _model;

        public GameSessionViewModel(GameSession model)
        {
            _model = model;

            CurrentLevel = new AutoUpdatedProperty<LevelViewModel, LevelLoadedEvent>(model.History, GetModel);
        }

        private LevelViewModel GetModel(LevelLoadedEvent arg)
        {
            return new LevelViewModel(arg.Level);
        }

    }
}