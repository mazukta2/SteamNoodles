using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public class ScreenManagerPresenter : BasePresenter<ScreenManagerView, ScreenManagerPresenter>
    {
        public Action<IScreenView> OnScreenOpened = delegate { };

        private readonly ScreenManagerView _view;
        private readonly IScreenAssets _screenAssets;

        public ScreenManagerPresenter(ScreenManagerView view, IScreenAssets screenAssets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenAssets = screenAssets ?? throw new ArgumentNullException(nameof(screenAssets));

            GetCollection<CommonScreens>().Open<MainScreenView>();
        }

        protected override void DisposeInner()
        {

        }

        public void Open<TScreen>(Func<TScreen, ScreenManagerPresenter, object> init) where TScreen : class, IScreenView
        {
            var screenPrefab = _screenAssets.GetScreen<TScreen>();
            if (screenPrefab == null)
                throw new Exception($"Cant find {typeof(TScreen).Name} view");

            _view.Screen.Clear();
            var view = (TScreen)_view.Screen.Spawn<TScreen>(screenPrefab);
            init(view, this);
            OnScreenOpened(view);
        }

        public TPreScreen GetCollection<TPreScreen>() where TPreScreen : ScreenCollection, new()
        {
            var preScreen = new TPreScreen();
            preScreen.SetManager(this);
            return preScreen;
        }

        public class CommonScreens : ScreenCollection
        {
            public void Open<TScreen>() where TScreen : class, IScreenView
            {
                Manager.Open<TScreen>(Init);

                object Init(TScreen screenView, ScreenManagerPresenter managerPresenter)
                {
                    if (screenView is MainScreenView mainScreen)
                        return new MainScreenPresenter(mainScreen, managerPresenter, mainScreen.Level.Model.Resources);

                    throw new Exception("Unknown screen " + typeof(TScreen));
                }
            }
        }
    }
}
