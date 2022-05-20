using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections
{

    public class BuildScreenCollection : ScreenCollection
    {
        public void Open(ConstructionCard constructionCard)
        {
            Manager.Open<IBuildScreenView>(Init);

            object Init(IBuildScreenView screenView, ScreenManagerPresenter managerPresenter)
            {
                return new BuildScreenPresenter(screenView, constructionCard,
                    IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IHandView.Default.Presenter, IBuildingToolitpView.Default.Presenter);
            }
        }
    }
}
