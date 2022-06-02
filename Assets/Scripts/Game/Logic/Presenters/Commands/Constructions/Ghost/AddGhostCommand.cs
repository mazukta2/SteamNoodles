using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class AddGhostCommand : IPresenterCommand
    {
        private EntityLink<ConstructionCard> _entityLink;
        private IViewContainer _tooltipContainer;
        private IViewPrefab _tooltipPrefab;

        public AddGhostCommand(EntityLink<ConstructionCard> entityLink,
            IViewContainer tooltipContainer, IViewPrefab tooltipPrefab)
        {
            _entityLink = entityLink;
            _tooltipContainer = tooltipContainer;
            _tooltipPrefab = tooltipPrefab;
        }

        public void Execute()
        {
            //var view = _view.Container.Spawn<IGhostView>(_view.GhostPrototype);
            //var ghost = new GhostPresenter(_settings, _screenManager, _fieldService, _buildingService, buildScreen,
            //    _controls, IGameKeysManager.Default, IGameAssets.Default, view, _time);
            //_ghost.OnGhostPostionChanged += UpdateGhostPosition;
        }
    }
}
