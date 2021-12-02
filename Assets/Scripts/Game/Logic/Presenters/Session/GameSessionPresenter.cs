using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Session
{
    public class GameSessionPresenter : Disposable
    {
        public LevelPresenter CurrentLevel { get; private set; }

        private GameSession _model;
        private IGameSessionView _view;

        public GameSessionPresenter(GameSession model, IGameSessionView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;

            _model.OnLoaded += _model_OnLoaded;
            _model.OnDispose += Dispose;
        }

        protected override void DisposeInner()
        {
            _model.OnLoaded -= _model_OnLoaded;
            _model.OnDispose -= Dispose;
            _view.Dispose();
            CurrentLevel?.Dispose();
        }

        private void _model_OnLoaded()
        {
            CurrentLevel = new LevelPresenter(_model.CurrentLevel, _view.CurrentLevel.Value);
        }

    }
}