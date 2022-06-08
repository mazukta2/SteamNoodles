using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views;
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
                if (screenView is IDayEndedScreenView dayEndedScreen)
                    return new DayEndedScreenPresenter(dayEndedScreen);

                throw new Exception("Unknown screen " + typeof(TScreen));
            }
        }
    }
}
