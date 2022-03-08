using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerView : View
    {
        public ViewContainer Screen;
        private ScreenManagerPresenter _presenter;

        protected override void CreatedInner()
        {
            var assets = CoreAccessPoint.Core.Engine.Assets.Screens;
            _presenter = new ScreenManagerPresenter(this, assets);
        }

        public void OnValidate()
        {
            if (Screen == null) throw new ArgumentNullException(nameof(Screen));
        }
    }
}
