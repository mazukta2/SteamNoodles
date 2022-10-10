using System;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Variations;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Variations;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens.Variations
{
    public class MainLevelView : ViewWithPresenter<MainLevelPresenter>, IMainLevelView
    {
        public MainLevelView(IViews collection) : base(collection)
        {
        }
    }
}

