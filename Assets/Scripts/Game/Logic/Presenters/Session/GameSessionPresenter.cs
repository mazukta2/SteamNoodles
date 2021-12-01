using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Session
{
    public class GameSessionPresenter
    {
        public LevelPresenter CurrentLevel { get; private set; }

        private GameSession _model;

        public GameSessionPresenter(GameSession model)
        {
            _model = model;
        }

        public void LoadLevel(ILevelPrototype levelPrototype)
        {
            levelPrototype.Load(OnFinished);
        }

        private void OnFinished(ILevelPrototype prototype, ILevelView view)
        {
            _model.SetLevel(new GameLevel(prototype, _model.Random));
            CurrentLevel = new LevelPresenter(_model.CurrentLevel, view);
        }
    }
}