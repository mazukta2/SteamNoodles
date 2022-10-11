using System;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Common
{
    public interface IExitGameButtonView : IViewWithGenericPresenter<ExitButtonPresenter>, IViewWithDefaultPresenter
    {
        IButton Button { get; }


        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new ExitButtonPresenter(this);
        }
    }
}

