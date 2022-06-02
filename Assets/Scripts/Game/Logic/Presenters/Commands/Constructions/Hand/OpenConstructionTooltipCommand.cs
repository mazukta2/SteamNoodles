using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class OpenConstructionTooltipCommand : IPresenterCommand
    {
        private EntityLink<ConstructionCard> _entityLink;
        private IViewContainer _tooltipContainer;
        private IViewPrefab _tooltipPrefab;

        public OpenConstructionTooltipCommand(EntityLink<ConstructionCard> entityLink,
            IViewContainer tooltipContainer, IViewPrefab tooltipPrefab)
        {
            _entityLink = entityLink;
            _tooltipContainer = tooltipContainer;
            _tooltipPrefab = tooltipPrefab;
        }

        public void Execute()
        {
            _tooltipContainer.Clear();
            var view = _tooltipContainer.Spawn<IHandConstructionTooltipView>(_tooltipPrefab);
            new HandConstructionTooltipPresenter(view).SetModel(_entityLink);
        }
    }
}
