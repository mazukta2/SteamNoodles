using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Building
{
    public class BuildConstructionCommand : IPresenterCommand
    {
        public Construction Construction { get; }
        public IViewContainer Container { get; }
        public IViewPrefab Prefab { get; }

        public BuildConstructionCommand(Construction construction,
            IViewContainer container, IViewPrefab prefab)
        {
            Construction = construction;
            Container = container;
            Prefab = prefab;
        }

        public void Execute()
        {
            //    //new ConstructionPresenter(_settings, arg1, _field, IGameAssets.Default, view, _ghostManager, IGameControls.Default);
            //var view = _view.Container.Spawn<IGhostView>(_view.GhostPrototype);
            //var ghost = new GhostPresenter(_settings, _screenManager, _fieldService, _buildingService, buildScreen,
            //    _controls, IGameKeysManager.Default, IGameAssets.Default, view, _time);
            //_ghost.OnGhostPostionChanged += UpdateGhostPosition;
        }
    }
}
