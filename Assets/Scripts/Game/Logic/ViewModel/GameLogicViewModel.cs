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

        public AutoUpdatedProperty<GameSessionViewModel, SessionCreatedEvent> Session { get; private set; }

        public GameLogicViewModel(GameLogic model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;

            Session = new AutoUpdatedProperty<GameSessionViewModel, SessionCreatedEvent>(model.History, GetModel);

        }

        private GameSessionViewModel GetModel(SessionCreatedEvent obj)
        {
            return new GameSessionViewModel(obj.Session);
        }
    }
}
