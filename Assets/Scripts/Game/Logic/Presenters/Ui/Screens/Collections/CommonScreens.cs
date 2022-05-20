using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections
{
    public class CommonScreens : ScreenCollection
    {
        public void Open<TScreen>() where TScreen : class, IScreenView
        {
            Manager.Open<TScreen>(Init);

            object Init(TScreen screenView, ScreenManagerPresenter managerPresenter)
            {
                if (screenView is IMainScreenView mainScreen)
                    return new MainScreenPresenter(mainScreen, managerPresenter,
                        IBattleLevel.Default.Constructions, IBattleLevel.Default.TurnManager, IHandView.Default.Presenter, IGameKeysManager.Default);

                if (screenView is IDayEndedScreenView dayEndedScreen)
                    return new DayEndedScreenPresenter(dayEndedScreen, IGameSession.Default, managerPresenter);

                if (screenView is IGameMenuScreenView gameMenuScreen)
                    return new GameMenuScreenPresenter(gameMenuScreen, IGameSession.Default, IGame.Default, IGameKeysManager.Default, managerPresenter);
                

                throw new Exception("Unknown screen " + typeof(TScreen));
            }
        }
    }
}
