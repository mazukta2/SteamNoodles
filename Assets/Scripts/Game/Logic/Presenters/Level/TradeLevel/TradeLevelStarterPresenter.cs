using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.TradeLevel
{
    public class TradeLevelStarterPresenter : Disposable, IPresenter
    {
        private Models.Levels.Variations.TradeLevel _level;

        public TradeLevelStarterPresenter(Models.Levels.Variations.TradeLevel level) 
        {
            _level = level;
            _level.OnStart += HandleOnStart;
            _level.OnDispose += Dispose;
        }

        protected override void DisposeInner()
        {
            _level.OnStart -= HandleOnStart;
            _level.OnDispose -= Dispose;
        }

        private void HandleOnStart()
        {
            IScreenManagerView.Default.Presenter.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

    }
}

