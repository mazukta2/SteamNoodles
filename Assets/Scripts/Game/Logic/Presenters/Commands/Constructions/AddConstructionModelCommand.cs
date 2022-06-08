using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions
{
    public class AddConstructionModelCommand : ICommand
    {
        public ConstructionScheme Scheme { get; }
        public IViewContainer Container { get; }

        public AddConstructionModelCommand(ConstructionScheme scheme,
            IViewContainer container)
        {
            Scheme = scheme;
            Container = container;
        }

        public void Execute()
        {
            //_constructionView.Container.Clear();
            //_modelView = _constructionView.Container.Spawn<IConstructionModelView>(assets.GetPrefab(_link.Get().Scheme.LevelViewPath));
            //_modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());

        }
    }
}
