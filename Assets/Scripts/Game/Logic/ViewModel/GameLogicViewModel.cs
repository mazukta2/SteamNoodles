using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Session;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel
{
    public class GameLogicViewModel
    {
        private GameLogic _model;
        private HistoryReader _historyReader;

        public GameLogicViewModel(GameLogic model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;

            _historyReader = new HistoryReader(model.History);
            _historyReader.Subscribe<SessionCreatedEvent>(UpdateSession);
        }

        public GameSessionViewModel Session { get; private set; }

        public void StartGame(ILevelPrototype levelPrototype)
        {
            _model.CreateSession();

            Session.LoadLevel(levelPrototype);
        }

        private void UpdateSession(SessionCreatedEvent obj)
        {
            Session = new GameSessionViewModel(obj.Session);
        }

    }
}
