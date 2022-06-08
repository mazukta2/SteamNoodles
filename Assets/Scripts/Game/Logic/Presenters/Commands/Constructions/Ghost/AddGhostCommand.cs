using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class AddGhostCommand : ICommand
    {
        public ConstructionCard Card { get; }
        public IViewContainer Container { get; }
        public IViewPrefab Prefab { get; }

        public AddGhostCommand(ConstructionCard card,
            IViewContainer container, IViewPrefab prefab)
        {
            Card = card;
            Container = container;
            Prefab = prefab;
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
