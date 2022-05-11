using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using System.Collections.Generic;
using System.Text;

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
                        ICurrentLevel.Default.Constructions, ICurrentLevel.Default.TurnManager, IHandView.Default.Presenter);

                throw new Exception("Unknown screen " + typeof(TScreen));
            }
        }
    }
}
