using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Common;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Session;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel
{
    public class GameLogicViewModel
    {
        private GameLogic _model;
        private HistoryReader _modelHistoryReader;

        public AutoUpdatedProperty<GameSessionViewModel> Session { get; private set; }

        public GameLogicViewModel(GameLogic model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;


            Session = new AutoUpdatedProperty<LevelViewModel, SessionCreatedEvent>(null, model.History, Update);
            Session = new AutoUpdatedProperty<GameSessionViewModel>(null, Update);

            _modelHistoryReader = new HistoryReader(_model.History);
            _modelHistoryReader.Subscribe<SessionCreatedEvent>(OnSessionCreatedHandle).Update();
        }

        private void OnSessionCreatedHandle(SessionCreatedEvent obj)
        {
            Session.Value = new GameSessionViewModel(obj.Session);
        }

        private void Update()
        {
            _modelHistoryReader.Update();
        }
    }
}
