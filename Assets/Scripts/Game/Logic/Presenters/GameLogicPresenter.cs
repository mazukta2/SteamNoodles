using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Session;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;

namespace Tests.Assets.Scripts.Game.Logic.Presenters
{
    public class GameLogicPresenter
    {
        private GameLogic _model;
        private HistoryReader _historyReader;

        public GameLogicPresenter(GameLogic model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;

            _historyReader = new HistoryReader(model.History);
            _historyReader.Subscribe<SessionCreatedEvent>(UpdateSession);
        }

        public GameSessionPresenter Session { get; private set; }

        public void StartGame(ILevelSettings levelPrototype)
        {
            _model.CreateSession();

            Session.LoadLevel(levelPrototype);
        }

        private void UpdateSession(SessionCreatedEvent obj)
        {
            Session = new GameSessionPresenter(obj.Session);
        }

    }
}
