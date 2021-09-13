using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Common;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Session
{
    public class GameSessionViewModel
    {
        private GameSession _model;
        private LevelViewModel _viewModel;

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
            _model.SetLevel(new GameLevel(prototype));
            _viewModel = new LevelViewModel(_model.CurrentLevel, view);
        }
    }
}