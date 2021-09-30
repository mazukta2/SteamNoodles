using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Session
{
    public class GameSessionViewModel
    {
        public LevelViewModel CurrentLevel { get; private set; }

        private GameSession _model;

        public GameSessionViewModel(GameSession model)
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
            CurrentLevel = new LevelViewModel(_model.CurrentLevel, view);
        }
    }
}