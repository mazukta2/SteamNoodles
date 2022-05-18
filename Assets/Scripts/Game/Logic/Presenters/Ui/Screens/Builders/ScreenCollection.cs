using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders
{
    public abstract class ScreenCollection
    {
        protected IScreenOpener Manager { get; private set; }
        public void SetManager(IScreenOpener manager)
        {
            Manager = manager;
        }
    }
}
