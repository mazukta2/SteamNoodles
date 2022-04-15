using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Views.Ui
{
    public abstract class ScreenView<T> : PresenterView<T>, IScreenView
        where T : IPresenter
    {
        protected ScreenView(LevelView level) : base(level)
        {
        }

    }
}
