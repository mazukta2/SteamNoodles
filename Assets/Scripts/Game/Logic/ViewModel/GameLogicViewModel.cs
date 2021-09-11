using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Common;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Session;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel
{
    public class GameLogicViewModel
    {
        private GameLogic _model;

        public GameLogicViewModel(GameLogic model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;

            Session = new AutoUpdatedProperty<GameSessionViewModel, SessionCreatedEvent>(model.History, GetModel);

        }

        public AutoUpdatedProperty<GameSessionViewModel, SessionCreatedEvent> Session { get; private set; }

        public void StartGame(ILevelPrototype levelPrototype)
        {
            _model.CreateSession();

            Session.Value.LoadLevel(levelPrototype);
        }

        private GameSessionViewModel GetModel(SessionCreatedEvent obj)
        {
            return new GameSessionViewModel(obj.Session);
        }
    }
}
