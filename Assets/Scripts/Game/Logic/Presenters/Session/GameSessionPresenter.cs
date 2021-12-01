using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Session
{
    public class GameSessionPresenter
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
        }

        public void LoadLevel(ILevelSettings levelPrototype)
        {
            levelPrototype.Load(OnFinished);
        }

        private void OnFinished(ILevelSettings prototype, ILevelView levelView)
        {
            _model.SetLevel(new GameLevel(prototype, _model.Random));
            _view.SetLevel(levelView);
            CurrentLevel = new LevelPresenter(_model.CurrentLevel, levelView);
        }
    }
}