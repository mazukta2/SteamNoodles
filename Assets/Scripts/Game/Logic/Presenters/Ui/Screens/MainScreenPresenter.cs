using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using Game.Assets.Scripts.Game.Logic.Services.Controls;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<IMainScreenView>
    {
        private IMainScreenView _view;
        private KeyCommand _exitKey;

        public MainScreenPresenter(IMainScreenView view) 
            : this(view,
                IPresenterServices.Default.Get<GameControlsService>())
        {

        }

        public MainScreenPresenter(IMainScreenView view, GameControlsService gameKeysManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
        }

        private void OnExitTap()
        {
            //ScreenManagerPresenter.Default.Open<IGameMenuScreenView>(x => new GameMenuScreenPresenter(x));
            //_commands.Execute(new OpenGameMenuScreenCommand());
        }
    }
}
