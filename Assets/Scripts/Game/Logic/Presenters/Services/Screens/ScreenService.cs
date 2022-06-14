using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens
{
    public class ScreenService : IService
    {
        public Action<IScreenView> OnScreenOpened = delegate { };
        private IScreenManagerView _view;
        private GameAssetsService _screenAssets;

        public ScreenService() : this(IPresenterServices.Default.Get<GameAssetsService>())
        {
        }

        public ScreenService(GameAssetsService screenAssets)
        {
            _screenAssets = screenAssets ?? throw new ArgumentNullException(nameof(screenAssets));
        }

        public void Bind(IScreenManagerView view)
        {
            if (_view != null) throw new ArgumentNullException(nameof(view));
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        public bool IsBinded()
        {
            return _view != null;
        }

        public TScreenView Open<TScreenView>(Action<TScreenView> setPresenter) where TScreenView : class, IScreenView
        {
            if (!IsBinded())
                throw new Exception("Service unbinded");

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
    }
}
