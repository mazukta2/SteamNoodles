using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Presenters.Session;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using System;

namespace Tests.Assets.Scripts.Game.Logic.Presenters
{
    public class GamePresenter : Disposable
    {
        public GameSessionPresenter Session { get; private set; }

        private readonly GameModel _model;
        private readonly IGameView _view;

        public GamePresenter(GameModel model, IGameView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

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
