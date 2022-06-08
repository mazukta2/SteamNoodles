using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class RemoveHandConstructionCommand : ICommand
    {
        private IViewContainer _container;

        public RemoveHandConstructionCommand(IViewContainer container)
        {
            _container = container;
        }

        public void Execute()
        {
            //var view = _container.Spawn<IHandConstructionView>(_prefab);
            //new HandConstructionPresenter(_entityLink, view);
        }
    }
}
