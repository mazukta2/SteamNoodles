using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders
{
    public abstract class ScreenCollection
    {
        protected ScreenManagerPresenter Manager { get; private set; }
        public void SetManager(ScreenManagerPresenter manager)
        {
            Manager = manager;
        }
    }
}
