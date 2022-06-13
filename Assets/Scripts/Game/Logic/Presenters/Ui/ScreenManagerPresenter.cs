using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public class ScreenManagerPresenter : BasePresenter<IScreenManagerView>, IScreenOpener
    {
        public static ScreenManagerPresenter Default { get; set; }

        public Action<IScreenView> OnScreenOpened = delegate { };


        private readonly IScreenManagerView _view;
        private readonly GameAssetsService _screenAssets;

        public ScreenManagerPresenter(IScreenManagerView view) : this(view, 
            IPresenterServices.Default.Get<GameAssetsService>())
        {
        }

        public ScreenManagerPresenter(IScreenManagerView view, GameAssetsService screenAssets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenAssets = screenAssets ?? throw new ArgumentNullException(nameof(screenAssets));
        }

        protected override void DisposeInner()
        {

        }

        public TScreenView Open<TScreenView>(Action<TScreenView> setPresenter) where TScreenView : class, IScreenView
        {
            var name = typeof(TScreenView).Name;
            name = name.Replace("View", "");
            name = name.Remove(0, 1);
            var screenPrefab = _screenAssets.GetPrefab($"Screens/{name}");
            if (screenPrefab == null)
                throw new Exception($"Cant find {name} view");

            _view.Screen.Clear();
            var view = _view.Screen.Spawn<TScreenView>(screenPrefab);
            setPresenter(view);
            OnScreenOpened(view);

            return view;
        }

        void IScreenOpener.Open<TScreen>(Func<TScreen, ScreenManagerPresenter, object> init)
        {
            var name = typeof(TScreen).Name;
            name = name.Replace("View", "");
            name = name.Remove(0, 1); 
            var screenPrefab = _screenAssets.GetPrefab($"Screens/{name}");
            if (screenPrefab == null)
                throw new Exception($"Cant find {name} view");

            _view.Screen.Clear();
            var view = _view.Screen.Spawn<TScreen>(screenPrefab);
            init(view, this);
            OnScreenOpened(view);
        }

        public TScreenCollection GetCollection<TScreenCollection>() where TScreenCollection : ScreenCollection, new()
        {
            var preScreen = new TScreenCollection();
            preScreen.SetManager(this);
            return preScreen;
        }

    }
}
