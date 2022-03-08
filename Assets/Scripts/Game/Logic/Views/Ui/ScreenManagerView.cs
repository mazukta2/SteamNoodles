using Game.Assets.Scripts.Game.Logic.Common.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using System;
#if UNITY
using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerView : View
    {
#if UNITY
        [SerializeField] private ViewContainer _screen;

        public void Clear()
        {
            _screen.Clear();
        }

        public TScreen Create<TScreen>(IScreenAsset<TScreen> screenAsset) where TScreen : ScreenView
        {
            return screenAsset.Create(_screen);
        }
        
        protected override void CreatedInner()
        {
            var assets = CoreAccessPoint.Core.Engine.Assets;
            _presenter = new ScreenManagerPresenter(this, assets);
        }
#else

        public ViewContainer Screen { get; set; } = new ViewContainer();

        public void Clear()
        {
            Screen.Clear();
        }

        public TScreen Create<TScreen>(IScreenAsset<TScreen> screenAsset) where TScreen : ScreenView
        {
            return screenAsset.Create(Screen);
        }
#endif

        private ScreenManagerPresenter _presenter;

        protected override void DisposeInner()
        {
#if !UNITY
            Screen.Dispose();
#endif
        }
    }
}
