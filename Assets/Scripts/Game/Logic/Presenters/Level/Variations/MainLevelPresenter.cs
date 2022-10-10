using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Variations
{
    public class MainLevelPresenter : BasePresenter<IMainLevelView>
    {
        private IMainLevelView _view;
        private GameLevel _level;

        public MainLevelPresenter(IMainLevelView view, GameLevel level) : base(view)
        {
            _view = view;
            _level = level;
            _level.OnStart += HandleOnStart;
        }

        protected override void DisposeInner()
        {
            _level.OnStart -= HandleOnStart;
        }

        private void HandleOnStart()
        {
            IScreenManagerView.Default.Presenter.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

    }
}
