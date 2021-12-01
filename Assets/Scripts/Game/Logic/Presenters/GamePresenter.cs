using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Session;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Common.Core;

namespace Tests.Assets.Scripts.Game.Logic.Presenters
{
    public class GamePresenter : Disposable
    {
        public GameSessionPresenter Session { get; private set; }

        private GameModel _model;
        private IGameView _view;

        public GamePresenter(GameModel model, IGameView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));
            _model = model;
            _view = view;

            if (_model.Session != null)
                CreatePresenter();

            _model.OnSessionCreated += CreatePresenter;
            _model.OnDispose += Dispose; 
        }

        protected override void DisposeInner()
        {
            _model.OnSessionCreated -= CreatePresenter;
            _model.OnDispose -= Dispose;
            Session?.Dispose();
            _view.Dispose();
        }

        private void CreatePresenter()
        {
            Session = new GameSessionPresenter(_model.Session, _view.CreateSession());
        }
    }
}
