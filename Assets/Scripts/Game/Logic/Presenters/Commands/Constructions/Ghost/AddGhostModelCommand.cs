using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Ghost
{
    public class AddGhostModelCommand : IPresenterCommand
    {
        public ConstructionScheme Scheme { get; }
        public IViewContainer Container { get; }

        public AddGhostModelCommand(ConstructionScheme scheme,
            IViewContainer tooltipContainer)
        {
            Scheme = scheme;
            Container = tooltipContainer;
        }

        public void Execute()
        {
            //_view.Container.Clear();
            //_modelView = _view.Container.Spawn<IConstructionModelView>(_assets.GetPrefab(card.Get().Scheme.LevelViewPath));
            //_modelView.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());
        }
    }
}
