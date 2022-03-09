using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public abstract class ScreenView : View
    {
    }

    public abstract class ScreenViewPresenter : ViewPresenter
    {
        protected ScreenViewPresenter(ILevel level) : base(level)
        {
        }

        public abstract void SetManager(ScreenManagerPresenter manager);
    }
}
