using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
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
                if (screenView is IDayEndedScreenView dayEndedScreen)
                    return new DayEndedScreenPresenter(dayEndedScreen, IGameSession.Default, managerPresenter);

                throw new Exception("Unknown screen " + typeof(TScreen));
            }
        }
    }
}
