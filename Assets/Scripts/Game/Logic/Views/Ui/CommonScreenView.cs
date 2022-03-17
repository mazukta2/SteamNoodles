using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public abstract class CommonScreenView : ScreenView
    {
        protected CommonScreenView(ILevel level) : base(level)
        {
        }

        public abstract void Init(ScreenManagerPresenter manager);
    }
}
