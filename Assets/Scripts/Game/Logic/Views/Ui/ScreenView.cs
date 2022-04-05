using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public abstract class ScreenView : View, IScreenView
    {
        public BaseGameScreenPresenter ScreenPresenter { get; set; }
        protected ScreenView(ILevel level) : base(level)
        {
        }

    }
}
