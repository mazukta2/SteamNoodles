using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class AddHandConstructionCommand : IPresenterCommand
    {
        private EntityLink<ConstructionCard> _entityLink;
        private IViewContainer _container;
        private IViewPrefab _prefab;

        public AddHandConstructionCommand(EntityLink<ConstructionCard> entityLink,
            IViewContainer tooltipContainer, IViewPrefab tooltipPrefab)
        {
            _entityLink = entityLink;
            _container = tooltipContainer;
            _prefab = tooltipPrefab;
        }

        public void Execute()
        {
            var view = _container.Spawn<IHandConstructionView>(_prefab);
            new HandConstructionPresenter(_entityLink, view);
        }
    }
}
