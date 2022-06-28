using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public class ScreenManagerPresenter : BasePresenter<IScreenManagerView>, IScreenOpener
    {
        public Action<IScreenView> OnScreenOpened = delegate { };

        private readonly IScreenManagerView _view;
        private readonly IAssets _screenAssets;

        public ScreenManagerPresenter(IScreenManagerView view, IAssets screenAssets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenAssets = screenAssets ?? throw new ArgumentNullException(nameof(screenAssets));
        }

        protected override void DisposeInner()
        {

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
