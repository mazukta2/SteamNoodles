using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Screens
{
    public class OpenBuildingScreenCommand : ICommand
    {
        public OpenBuildingScreenCommand(ConstructionCard card)
        {
            Card = card;
        }

        public ConstructionCard Card { get; set; }

        public void Execute()
        {
            ScreenManagerPresenter.Default.Open<IBuildScreenView>(view => new BuildScreenPresenter(view, Card));
        }
    }
}
