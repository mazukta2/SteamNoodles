using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Screens
{
    public class BuildScreenViewPresenter : ScreenViewPresenter
    {
        private BuildScreenPresenter _presenter;
        public BuildScreenViewPresenter(ILevel level) : base(level)
        {
        }

        public override void SetManager(ScreenManagerPresenter manager)
        {
            base.SetManager(manager);
            _presenter = new BuildScreenPresenter(this, manager);
        }
    }
}
