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

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Variations
{
    public class MainLevelPresenter : BasePresenter<IMainLevelView>
    {
        private IMainLevelView _view;

        public MainLevelPresenter(IMainLevelView view) : base(view)
        {
            _view = view;
        }


        private void Start()
        {
            IScreenManagerView.Default.Presenter.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }
    }
}
