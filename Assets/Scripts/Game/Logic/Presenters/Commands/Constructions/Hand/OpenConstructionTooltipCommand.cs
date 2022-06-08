using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class OpenConstructionTooltipCommand : ICommand
    {
        public ConstructionCard Card { get; }
        public IViewContainer Container { get; }
        public IViewPrefab Prefab { get; }

        public OpenConstructionTooltipCommand(ConstructionCard card,
            IViewContainer tooltipContainer, IViewPrefab tooltipPrefab)
        {
            Card = card;
            Container = tooltipContainer;
            Prefab = tooltipPrefab;
        }

        public void Execute()
        {
            Container.Clear();
            var view = Container.Spawn<IHandConstructionTooltipView>(Prefab);
            new HandConstructionTooltipPresenter(view).SetModel(Card);
        }
    }
}
