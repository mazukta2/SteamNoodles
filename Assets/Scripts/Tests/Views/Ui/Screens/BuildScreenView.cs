using Assets.Scripts.Tests.Views.Ui.Screens.Elements;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Assets.Scripts.Tests.Views.Ui;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView<BuildScreenPresenter>, IBuildScreenView
    {

        public BuildScreenView(IViewsCollection level) : base(level)
        {

        }

        public void Init(ConstructionCard card)
        {

        }
    }
}
