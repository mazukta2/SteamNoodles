using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<IMainScreenView>
    {
        private IMainScreenView _view;
        private readonly ICommands _commands;
        private KeyCommand _exitKey;

        public MainScreenPresenter(IMainScreenView view) 
            : this(view, IGameKeysManager.Default, ICommands.Default)
        {

        }

        public MainScreenPresenter(IMainScreenView view, IGameKeysManager gameKeysManager, ICommands commands) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _commands = commands;
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;
        }

        protected override void DisposeInner()
        {
            _exitKey.OnTap -= OnExitTap;
        }

        private void OnExitTap()
        {
            _commands.Execute(new OpenGameMenuScreenCommand());
        }
    }
}
