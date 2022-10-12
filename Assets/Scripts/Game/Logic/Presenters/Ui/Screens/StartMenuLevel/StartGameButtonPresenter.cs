using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Views.Levels.StartMenuLevels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.StartMenuLevel
{
    public class StartGameButtonPresenter : Disposable, IPresenter
    {
        private StartMenu _currentLevel;

        public StartGameButtonPresenter(IStartNewGameView view) : this (view, IModels.Default.Find<StartMenu>())
        {

        }

        public StartGameButtonPresenter(IStartNewGameView view, StartMenu level)
        {
            _currentLevel = level;
            view.Button.SetAction(StartGame);
        }

        private void StartGame()
        {
            _currentLevel.StartCutscene();
        }
    }
}

