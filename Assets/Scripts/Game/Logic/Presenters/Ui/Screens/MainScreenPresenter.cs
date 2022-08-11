using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BasePresenter<IMainScreenView>
    {
        private ScreenManagerPresenter _screenManager;
        private FlowManager _turnManager;
        private readonly HandPresenter _handPresenter;
        private KeyCommand _exitKey;

        public MainScreenPresenter(IMainScreenView view, ScreenManagerPresenter screenManager,
            FlowManager turnManager, HandPresenter handPresenter, IGameKeysManager gameKeysManager) : base(view)
        {
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _turnManager = turnManager ?? throw new ArgumentNullException(nameof(turnManager));
            _handPresenter = handPresenter;
            _handPresenter.Mode = HandPresenter.Modes.Choose;

            _turnManager.OnDayFinished += HandleOnDayFinished;
            _exitKey = gameKeysManager.GetKey(GameKeys.Exit);
            _exitKey.OnTap += OnExitTap;

            WaveWidgetPresenter.SetEnabled(true);
            CustumerCoinsPresenter.SetEnabled(true);
        }

        protected override void DisposeInner()
        {
            _turnManager.OnDayFinished -= HandleOnDayFinished;
            _exitKey.OnTap -= OnExitTap;
            _handPresenter.Mode = HandPresenter.Modes.Disabled;

            WaveWidgetPresenter.SetEnabled(false);
            CustumerCoinsPresenter.SetEnabled(false);
        }

        private void HandleOnDayFinished()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IDayEndedScreenView>();
        }

        private void OnExitTap()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IGameMenuScreenView>();
        }

    }
}
