using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public class ScreenManagerPresenter : BasePresenter
    {
        public Action<BaseGameScreenPresenter> OnScreenOpened = delegate { };

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

        public void Open<TScreen>(Action<TScreen, ScreenManagerPresenter> init) where TScreen : ScreenView
        {
            var screenPrefab = _screenAssets.GetScreen<TScreen>();
            if (screenPrefab == null)
                throw new Exception($"Cant find {typeof(TScreen).Name} view");

            _view.Screen.Clear();
            var view = (TScreen)screenPrefab.Create<TScreen>(_view.Screen);
            init(view, this);
            OnScreenOpened(view.Presenter);
        }

        public TPreScreen GetCollection<TPreScreen>() where TPreScreen : ScreenCollection, new()
        {
            var preScreen = new TPreScreen();
            preScreen.SetManager(this);
            return preScreen;
        }

        public class CommonScreens : ScreenCollection
        {
            public void Open<TScreen>() where TScreen : CommonScreenView
            {
                Manager.Open<TScreen>(Init);

                void Init(TScreen screenView, ScreenManagerPresenter managerPresenter)
                {
                    screenView.Init(managerPresenter);
                }
            }
        }
    }
}
