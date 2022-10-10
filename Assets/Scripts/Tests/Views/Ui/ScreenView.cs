using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

namespace Game.Assets.Scripts.Tests.Views.Ui
{
    public abstract class ScreenView<T> : ViewWithPresenter<T>, IScreenView
        where T : IPresenter
    {
        protected ScreenView(IViews level) : base(level)
        {
        }

    }
}
