using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Screens
{
    public class OpenBuildingScreenCommand : IPresenterCommand
    {
        private EntityLink<ConstructionCard> _entityLink;

        public OpenBuildingScreenCommand(EntityLink<ConstructionCard> entityLink)
        {
            _entityLink = entityLink;
        }

        public void Execute()
        {
            ScreenManagerPresenter.Default.Open<IBuildScreenView>(view => new BuildScreenPresenter(view, _entityLink));
        }
    }
}
