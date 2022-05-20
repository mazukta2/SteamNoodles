using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets
{
    public class BuildingTooltipMock : ViewWithPresenter<BuildingTooltipPresenter>, IBuildingToolitpView
    {
        public IHandConstructionTooltipView Tooltip { get; set; }
        public IAnimator Animator { get; set; } = new AnimatorMock();
        public BuildingTooltipMock(IViewsCollection collection) : base(collection)
        {
            Tooltip = new HandConstructionTooltipView(collection);
        }
    }
}
